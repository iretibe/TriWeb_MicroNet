using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Core.ValueObjects;
using MicroNet.User.Infrastructure.Clients.Branch;
using MicroNet.User.Infrastructure.Clients.Menu;
using MicroNet.User.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly UserContext _context;

        private readonly MenuServiceClient _menuService;
        private readonly BranchServiceClient _branchService;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserGroupRepository(UserContext context,
            MenuServiceClient menuService, BranchServiceClient branchService,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _menuService = menuService;
            _branchService = branchService;
            _roleManager = roleManager;
        }

        public async Task<AddUserGroupDto> AddUserGroupsAsync(AddUserGroupDto entity)
        {
            // Construct audit info
            var auditInfo = new AuditInfo(entity.CreatedBy, DateTime.UtcNow, null!, null, null!, null);

            // Construct working hours
            var workingHours = new TimeRange(TimeSpan.Parse(entity.StartTime), TimeSpan.Parse(entity.EndTime));

            // Create the UserGroup aggregate root using the constructor
            var userGroup = new UserGroup(entity.UserGroupName, entity.BranchId, workingHours, auditInfo);

            // Add working days
            foreach (var day in entity.WorkingDays)
            {
                userGroup.AddWorkingDay(Enum.Parse<DayOfWeek>(day));
            }

            // Add menus and submenus
            foreach (var menu in entity.Menus)
            {
                userGroup.AddMenu(menu.MenuId, menu.IsChecked, auditInfo);

                foreach (var submenu in menu.AddSubMenuDto)
                {
                    userGroup.AddSubMenu(menu.MenuId, submenu.SubMenuId, submenu.IsChecked);
                }
            }

            // Add the aggregate root to the DbContext
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task AddUserRoles(string Id, string RoleName, string NormalizedName)
        {
            // Check if role exists, if not create it
            var role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                var createRoleResult = await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = RoleName,
                    NormalizedName = RoleName.ToUpper()
                });

                if (!createRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", createRoleResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to create role: {errors}");
                }
            }
        }

        public async Task<bool> CheckUserGroupNameExistsAsync(string userGroupName)
        {
            return await _context.UserGroups.AnyAsync(ug => ug.UserGroupName == userGroupName);
        }

        public async Task DeleteUserGroupAsync(Guid userGroupId)
        {
            var model = await _context.UserGroups.SingleOrDefaultAsync(c => c.Id == userGroupId);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserGroupDto>> GetAllUserGroupsAsync()
        {
            var query = await (from ug in _context.UserGroups
                               //join b in _context.Branches on ug.BranchId equals b.Id
                               select new UserGroupDto
                               {
                                   Id = ug.Id,
                                   UserGroupName = ug.UserGroupName,
                                   IsActive = ug.IsActive,
                                   BranchId = ug.BranchId,
                                   StartTime = ug.WorkingHours.Start,
                                   EndTime = ug.WorkingHours.End,
                                   //BranchName = b.Name
                               }).ToListAsync();

            return query;
        }

        public async Task<List<GetMenuDto>> GetMenusByUserGroupIdAsync(Guid userGroupId)
        {
            //Fetch full system menus from MenuService
            var allMenus = await _menuService.GetAllMenusAsync();

            //Fetch selected menus and submenus for the user group
            var selectedMenus = await _context.UserGroupMenus
                .Where(x => x.UserGroupId == userGroupId)
                .ToListAsync();

            var selectedSubMenus = await _context.UserSubGroupMenus
                .Where(x => x.UserGroupId == userGroupId)
                .ToListAsync();

            //Combine data into a response
            var result = allMenus.Select(menu => new GetMenuDto
            {
                MenuId = menu.Id,
                MenuName = menu.Name,
                UserGroupId = userGroupId,
                IsChecked = selectedMenus.Any(sm => sm.MenuId == menu.Id && sm.IsChecked),

                SubMenuDto = menu.SubMenuDto.Select(sub => new GetSubMenuDto
                {
                    MenuId = sub.MenuId,
                    SubMenuId = sub.Id,
                    MenuName = menu.Name,
                    SubMenuName = sub.Name,
                    UserGroupId = userGroupId,
                    IsChecked = selectedSubMenus.Any(ssm =>
                        ssm.MenuId == sub.MenuId &&
                        ssm.SubMenuId == sub.Id &&
                        ssm.IsChecked)
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<List<GetSubMenuDto>> GetSubMenusByMenuIdAsync(Guid userGroupId, Guid menuId)
        {
            //Fetch all menus from MenuService (includes submenus)
            var allMenus = await _menuService.GetAllMenusAsync();

            //Get the specific parent menu from the full list
            var targetMenu = allMenus.FirstOrDefault(m => m.Id == menuId);

            if (targetMenu == null)
                return new List<GetSubMenuDto>();

            //Fetch selected submenus from local DB for the given menu and user group
            var selectedSubMenus = await _context.UserSubGroupMenus
                .Where(x => x.UserGroupId == userGroupId && x.MenuId == menuId)
                .ToListAsync();

            //Combine and return
            var subMenuDtos = targetMenu.SubMenuDto.Select(sub => new GetSubMenuDto
            {
                MenuId = sub.MenuId,
                SubMenuId = sub.Id,
                MenuName = targetMenu.Name,
                SubMenuName = sub.Name,
                UserGroupId = userGroupId,
                IsChecked = selectedSubMenus.Any(ssm =>
                    ssm.MenuId == sub.MenuId &&
                    ssm.SubMenuId == sub.Id &&
                    ssm.IsChecked)
            }).ToList();

            return subMenuDtos;
        }

        public async Task<GetUserGroupDto> GetUserGroupByIdAsync(Guid userGroupId)
        {
            //Fetch the user group
            var userGroupEntity = await _context.UserGroups
                .FirstOrDefaultAsync(ug => ug.Id == userGroupId);

            if (userGroupEntity == null)
                return null!;

            //Fetch branch name from BranchService
            var branch = await _branchService.GetBranchByIdAsync(userGroupEntity.BranchId);

            //Initialize response
            var userGroup = new GetUserGroupDto
            {
                Id = userGroupEntity.Id,
                UserGroupName = userGroupEntity.UserGroupName,
                StartTime = userGroupEntity.WorkingHours.Start,
                EndTime = userGroupEntity.WorkingHours.End,
                IsActive = userGroupEntity.IsActive,
                BranchId = userGroupEntity.BranchId,
                BranchName = branch.BranchName // Use null conditional in case not found
            };

            //Get all menus from MenuService
            var allMenus = await _menuService.GetAllMenusAsync(); // Assume it includes both menus and submenus

            //Get assigned user group menus
            var assignedMenus = await _context.UserGroupMenus
                .Where(ugm => ugm.UserGroupId == userGroupId)
                .ToListAsync();

            //Get assigned user group submenus
            var assignedSubMenus = await _context.UserSubGroupMenus
                .Where(usgm => usgm.UserGroupId == userGroupId)
                .ToListAsync();

            //Filter top-level menus
            var menuDtos = (from ugm in assignedMenus
                            join menu in allMenus on ugm.MenuId equals menu.Id
                            orderby menu.MenuOrderId
                            select new GetMenuDto
                            {
                                UserGroupId = ugm.UserGroupId,
                                MenuId = ugm.MenuId,
                                MenuName = menu.Name,
                                IsChecked = ugm.IsChecked,
                                SubMenuDto = assignedSubMenus
                                    .Where(sm => sm.MenuId == ugm.MenuId)
                                    .Join(allMenus,
                                          sm => sm.SubMenuId,
                                          m => m.Id,
                                          (sm, subMenu) => new GetSubMenuDto
                                          {
                                              UserGroupId = sm.UserGroupId,
                                              MenuId = sm.MenuId,
                                              MenuName = menu.Name,
                                              SubMenuId = sm.SubMenuId,
                                              SubMenuName = subMenu.Name,
                                              IsChecked = sm.IsChecked
                                          }).OrderBy(x => x.SubMenuName).ToList()
                            }).ToList();

            userGroup.MenuDto = menuDtos;

            //Fetch working days
            userGroup.WorkingDaysDto = await _context.UserGroupWorkingDays
                .Where(wd => wd.UserGroupId == userGroupId)
                .OrderBy(wd => wd.DayOfWeek)
                .Select(wd => new GetWorkingDayDto
                {
                    DayOfWeek = wd.DayOfWeek,
                    DayName = wd.DayOfWeek.ToString()
                }).ToListAsync();

            return userGroup;
        }

        public async Task<UserGroup> GetUserGroupByNameAsync(string userGroupName, Guid branchId)
        {
            var list = await _context
                .UserGroups.Where(c => c.UserGroupName == userGroupName && c.BranchId == branchId).FirstOrDefaultAsync();
            
            return list!;
        }

        public async Task<IEnumerable<UserGroupMenu>> GetUserGroupMenuByUniqueIdAsync(Guid userGroupId)
        {
            var list = await _context
                .UserGroupMenus.Where(c => c.UserGroupId == userGroupId).ToListAsync();
            
            return list;
        }

        public async Task<List<GetWorkingDayDto>> GetWorkingDaysByUserGroupIdAsync(Guid userGroupId)
        {
            return await _context.UserGroupWorkingDays
                .Where(ugwd => ugwd.UserGroupId == userGroupId)
                .Select(ugwd => new GetWorkingDayDto
                {
                    DayOfWeek = ugwd.DayOfWeek,
                    DayName = ugwd.DayOfWeek.ToString()
                })
                .ToListAsync();
        }

        public async Task UpdateUserGroupAsync(UserGroup entity)
        {
            _context.UserGroups.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserGroupMenusAsync(UpdateUserGroupDto entity)
        {
            var existingUserGroup = await _context.UserGroups
                .Include(x => x.WorkingDays)
                .Include(x => x.Menus)
                .Include(x => x.SubMenus)
                .FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingUserGroup == null)
                return false;

            ////Update main fields
            //existingUserGroup.UserGroupName = entity.UserGroupName;
            //existingUserGroup.BranchId = entity.BranchId;
            //existingUserGroup.IsActive = entity.IsActive;
            //existingUserGroup.WorkingHours = new TimeRange(
            //    TimeSpan.Parse(entity.StartTime),
            //    TimeSpan.Parse(entity.EndTime)
            //);
            //existingUserGroup.AuditInfo = new AuditInfo
            //{
            //    CreatedBy = entity.CreatedBy,
            //    CreatedAt = DateTime.UtcNow
            //};

            // Clear existing working days, menus, submenus
            _context.UserGroupWorkingDays.RemoveRange(
                _context.UserGroupWorkingDays.Where(x => x.UserGroupId == entity.Id)
            );

            _context.UserGroupMenus.RemoveRange(
                _context.UserGroupMenus.Where(x => x.UserGroupId == entity.Id)
            );

            _context.UserSubGroupMenus.RemoveRange(
                _context.UserSubGroupMenus.Where(x => x.UserGroupId == entity.Id)
            );

            // Add new working days
            foreach (var day in entity.WorkingDays)
            {
                _context.UserGroupWorkingDays.Add(new UserGroupWorkingDay(
                    entity.Id, day.DayOfWeek, entity.CreatedBy
                ));

                // Domain-level collection (optional)
                existingUserGroup.AddWorkingDay((DayOfWeek)day.DayOfWeek);
            }

            // Add new menus and submenus
            foreach (var menuGroup in entity.Menus)
            {
                var audit = new AuditInfo(
                    createdBy: null!,
                    createdAt: null,
                    updatedBy: entity.UpdatedBy,
                    updateAt: DateTime.UtcNow,
                    deletedBy: null!,
                    deletedAt: null
                );

                existingUserGroup.AddMenu(menuGroup.MenuId, menuGroup.IsChecked, audit);

                foreach (var sub in menuGroup.UpdSubMenuDto)
                {
                    existingUserGroup.AddSubMenu(menuGroup.MenuId, sub.SubMenuId, sub.IsChecked);
                }
            }

            _context.UserGroups.Update(existingUserGroup);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
