using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Clients.Branch;
using MicroNet.User.Infrastructure.Clients.Menu;
using MicroNet.User.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly UserContext _context;

        private readonly MenuServiceClient _menuService;
        private readonly BranchServiceClient _branchService;

        private readonly RoleManager<IdentityRole> _roleManager;

        public UserAccessRepository(UserContext context,
            MenuServiceClient menuService, BranchServiceClient branchService,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _menuService = menuService;
            _branchService = branchService;
            _roleManager = roleManager;
        }

        public async Task<UserMenuAccess> AddBulkUserMenuAccessAsync(UserMenuAccess entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserMenuAccess> AddUserMenuAccess(UserMenuAccess entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AssignedMenusDto1> AssignMenusToUserAsync(AssignedMenusDto1 entity)
        {
            //var userId = Guid.Parse(entity.UserId);

            // Remove existing entries for the user
            var existingEntries = _context.UserMenuAccesses.Where(x => x.UserId == entity.UserId);
            _context.UserMenuAccesses.RemoveRange(existingEntries);

            // Add new entries
            foreach (var menu in entity.AssignedMenus)
            {
                //var userMenuAccess = new UserMenuAccess
                //{
                //    Id = Guid.NewGuid(),
                //    UserId = entity.UserId,
                //    MenuId = menu.Id,
                //    IsChecked = menu.IsChecked
                //};
                var userMenuAccess = new UserMenuAccess(entity.UserId, menu.Id, menu.IsChecked);

                _context.UserMenuAccesses.Add(userMenuAccess);

                foreach (var subMenu in menu.subMenuDto)
                {
                    //var userSubMenuAccess = new UserMenuAccess
                    //{
                    //    Id = Guid.NewGuid(),
                    //    UserId = entity.UserId,
                    //    MenuId = subMenu.Id,
                    //    IsChecked = subMenu.IsChecked
                    //};
                    var userSubMenuAccess = new UserMenuAccess(entity.UserId, subMenu.Id, subMenu.IsChecked);

                    _context.UserMenuAccesses.Add(userSubMenuAccess);
                }
            }

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<AssignedMenusDto1> GetAllAssignedSystemMenusByUserIdAsync(string userId)
        {
            var allMenus = await _menuService.GetAllMenuEntitiesAsync();

            var userMenuAccess = await _context.UserMenuAccesses
                .Where(uma => uma.UserId == userId)
                .ToListAsync();

            var assignedMenuIds = userMenuAccess.Select(uma => uma.MenuId).ToHashSet();

            var menusWithSubMenus = allMenus
                .Where(menu => menu.ParentId == Guid.Empty)
                .Select(menu => new MenuDto1
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    subMenuDto = allMenus
                        .Where(subMenu => subMenu.ParentId == menu.Id && assignedMenuIds.Contains(subMenu.Id))
                        .Select(subMenu => new SubMenuDto1
                        {
                            Id = subMenu.Id,
                            Name = subMenu.Name,
                            MenuId = subMenu.ParentId
                        }).ToList()
                }).ToList();

            return new AssignedMenusDto1
            {
                UserId = userId,
                AssignedMenus = menusWithSubMenus
                    .Where(menu => assignedMenuIds.Contains(menu.Id) ||
                                   menu.subMenuDto.Any(subMenu => assignedMenuIds.Contains(subMenu.Id)))
                    .ToList()
            };
        }

        public async Task<IEnumerable<MenuEntityDto>> GetAllMenuListAsync()
        {
            //return await _context.Menus
            //    .Include(m => m.SubMenus).OrderBy(m => m.MenuOrderId).ToListAsync();

            var allMenus = await _menuService.GetAllMenuEntitiesAsync();

            return allMenus;
        }

        public async Task<List<MenuEntityDto>> GetMenusForUser(string userId)
        {
            var menuIds = await _context.UserMenuAccesses
                .Where(uma => uma.UserId == userId)
                .Select(uma => uma.MenuId)
                .ToListAsync();

            var allMenus = await _menuService.GetAllMenuEntitiesAsync();

            return allMenus
                .Where(m => menuIds.Contains(m.Id))
                .ToList();
        }

        public async Task<AssignedMenusForUpdateDto> GetUserSystemMenusForUpdateAsync(string userId)
        {
            var rootMenuParentId = Guid.Parse("00000000-0000-0000-0000-000000000000");

            var allMenus = await _menuService.GetAllMenuEntitiesAsync();

            // Fetch root menus
            var rootMenus = await (from uma in _context.UserMenuAccesses
                                   join m in allMenus on uma.MenuId equals m.Id
                                   where uma.UserId == userId && m.ParentId == rootMenuParentId
                                   orderby m.MenuOrderId
                                   select new MenuForUpdateDto
                                   {
                                       Id = uma.MenuId,
                                       Name = m.Name,
                                       IsChecked = uma.IsChecked,
                                       SubMenuDto = new List<SubMenuForUpdateDto>()
                                   }).ToListAsync();

            if (rootMenus != null && rootMenus.Any())
            {
                foreach (var menu in rootMenus)
                {
                    // Fetch submenus for each root menu
                    var subMenus = await (from sm in _context.UserMenuAccesses
                                          join m in allMenus on sm.MenuId equals m.Id
                                          where sm.UserId == userId && m.ParentId == menu.Id
                                          orderby m.MenuOrderId
                                          select new SubMenuForUpdateDto
                                          {
                                              Id = sm.MenuId,
                                              Name = m.Name,
                                              MenuId = m.ParentId,
                                              IsChecked = sm.IsChecked
                                          }).ToListAsync();

                    // Assign submenus to the corresponding MenuDto
                    menu.SubMenuDto = subMenus;
                }
            }

            return new AssignedMenusForUpdateDto
            {
                UserId = userId,
                AssignedMenus = rootMenus!
            };
        }

        public async Task<bool> UpdateAssignMenusToUserAsync(AssignedUserMenusForUpdateDto entity)
        {
            var userId = entity.UserId;

            // Remove existing entries for the user
            var existingEntries = _context.UserMenuAccesses.Where(x => x.UserId == userId);
            _context.UserMenuAccesses.RemoveRange(existingEntries);

            // Add new entries
            foreach (var menu in entity.AssignedMenus)
            {
                var userMenuAccess = new UserMenuAccess(userId, menu.Id, menu.IsChecked);

                _context.UserMenuAccesses.Add(userMenuAccess);

                foreach (var subMenu in menu.SubMenuDto)
                {
                    var userSubMenuAccess = new UserMenuAccess(userId, subMenu.Id, subMenu.IsChecked);
                    userSubMenuAccess.SetAuditInfo(userId);

                    _context.UserMenuAccesses.Add(userSubMenuAccess);
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
