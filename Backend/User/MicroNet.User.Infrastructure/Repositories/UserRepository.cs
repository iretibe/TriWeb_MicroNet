using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Helper;
using MicroNet.User.Core.Models;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityUserContext _context;
        private readonly UserContext _userContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(IdentityUserContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            UserContext userContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userContext = userContext;
        }

        public async Task<IdentityResult> AddRoleToUsersAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                    return roleResult;
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            return result;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var entity = await _context.AspNetUsers.SingleOrDefaultAsync(u => u.Id == userId);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetAllSysAdministratorsDto>> GetAllSysAdministrators()
        {
            var aspNetUsers = await _context.AspNetUsers
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.PhoneNumber
                }).ToListAsync();

            var userPermissions = await (from up in _userContext.UserPermissions
                                         let upGroupId = up.UserGroupId
                                         from ug in _userContext.UserGroups
                                         where ug.Id == upGroupId && ug.UserGroupName == "System Administrator"
                                         select new
                                         {
                                             up.Id,
                                             up.UserId,
                                             up.UserGroupId,
                                             UserGroupName = ug.UserGroupName
                                         }).ToListAsync();

            var result = from up in userPermissions
                         join u in aspNetUsers on up.UserId equals u.Id
                         select new GetAllSysAdministratorsDto
                         {
                             Id = up.Id,
                             UserId = up.UserId,
                             UserGroupId = up.UserGroupId,
                             FullName = u.FullName,
                             Email = u.Email,
                             PhoneNumber = u.PhoneNumber,
                             UserGroupName = up.UserGroupName
                         };

            return result;
        }

        public async Task<IEnumerable<SysAdminRecordsDto>> GetAllSysAdminUsersAsync()
        {
            var users = await _context.AspNetUsers
                .Select(u => new SysAdminRecordsDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Status = u.Status,
                    StatusName = u.Status ? "Active" : "Inactive",
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsSystemAdmin = u.IsSystemAdmin,
                    RoleName = (from aur in _context.AspNetUserRoles
                                join ar in _context.AspNetRoles on aur.RoleId equals ar.Id
                                where aur.UserId == u.Id
                                select ar.Name).FirstOrDefault()
                })
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<UserRecordsDto>> GetAllUsersAsync()
        {
            var users = await _context.AspNetUsers
                .Select(u => new UserRecordsDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    PhysicalAddress = u.PhysicalAddress,
                    PostalAddress = u.PostalAddress,
                    UserImage = u.UserImage,
                    CreateDate = u.CreateDate,
                    CreateBy = u.CreateBy,
                    AddedByName = _context.AspNetUsers
                        .Where(x => x.Id == u.CreateBy.ToString())
                        .Select(x => x.FullName)
                        .FirstOrDefault(),

                    Status = u.Status,
                    StatusName = u.Status ? "Active" : "Inactive",
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsSystemAdmin = u.IsSystemAdmin,

                    RoleName = (from aur in _context.AspNetUserRoles
                                join ar in _context.AspNetRoles on aur.RoleId equals ar.Id
                                where aur.UserId == u.Id
                                select ar.Name).FirstOrDefault()
                })
                .ToListAsync();

            return users;
        }

        public async Task<AspNetUsers> GetUserByEmailAsync(string email)
        {
            var list = await _context.AspNetUsers.Where(u => u.Email == email).FirstOrDefaultAsync();

            return list!;
        }

        public async Task<AspNetUsers> GetUserByIdAsync(string id)
        {
            var list = await _context.AspNetUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            return list!;
        }

        public async Task<AspNetUsers> GetUserByNameAsync(string name)
        {
            var list = await _context.AspNetUsers.Where(u => u.UserName == name).FirstOrDefaultAsync();

            return list!;
        }

        public async Task UpdateIsFirstTimeLogin(bool IsFirstTimeLogin, string UserId)
        {
            var entity = await _context.AspNetUsers.Where(u => u.Id == UserId).FirstOrDefaultAsync();

            entity!.Id = UserId;
            entity.IsFirstTimeLogin = true;

            _context.AspNetUsers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(AspNetUsers user)
        {
            var entity = await _context.AspNetUsers.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            entity!.Id = user.Id;
            entity.PhysicalAddress = user.PhysicalAddress;
            entity.PostalAddress = user.PostalAddress;
            entity.UserImage = user.UserImage;
            entity.IsSystemAdmin = user.IsSystemAdmin;

            _context.AspNetUsers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserStatusById(bool Status, string UserId)
        {
            var entity = await _context.AspNetUsers.Where(u => u.Id == UserId).FirstOrDefaultAsync();

            entity!.Id = UserId;
            entity.Status = true;

            _context.AspNetUsers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserWithRoleAsync(string userId, string roleName)
        {
            // Get the role
            var role = await _context.AspNetRoles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
                throw new Exception($"Role '{roleName}' not found.");

            // Check if user already has the role
            var existingRole = await _context.AspNetUserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == role.Id);

            if (existingRole)
                return; // Already assigned

            // Assign role
            //var userRole = new IdentityUserRole<string>
            //{
            //    UserId = userId,
            //    RoleId = role.Id
            //};

            var userRole = new AspNetUserRoles
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RoleId = role.Id
            };

            _context.AspNetUserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
