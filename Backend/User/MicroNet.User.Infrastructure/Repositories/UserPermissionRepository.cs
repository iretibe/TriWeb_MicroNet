using MicroNet.User.Core.Dto.Permission;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Models;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Clients.Branch;
using MicroNet.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly UserContext _context;
        private readonly IdentityUserContext _userContext;
        private readonly BranchServiceClient _branchService;

        public UserPermissionRepository(UserContext context, IdentityUserContext userContext,
            BranchServiceClient branchService)
        {
            _context = context;
            _userContext = userContext;
            _branchService = branchService;
        }

        public async Task<UserPermission> AddUserPermissionAsync(UserPermission entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteUserPermissionAsync(Guid userPermissionId)
        {
            var model = await _context.UserPermissions.SingleOrDefaultAsync(c => c.Id == userPermissionId);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserPermissionDto>> GetAllUserPermissionsAsync()
        {
            var userPermissions = await _context.UserPermissions
                .Select(up => new
                {
                    up.Id,
                    up.UserId,
                    up.UserGroupId,
                    up.BranchId,
                    up.RoleName,
                    up.AuditInfo.CreatedAt,
                    up.AuditInfo.CreatedBy,
                    up.AuditInfo.UpdatedAt,
                    up.AuditInfo.UpdatedBy
                })
                .ToListAsync();

            // Get distinct user IDs to fetch user info in batch
            var userIds = userPermissions
                .SelectMany(up => new[] { up.UserId, up.CreatedBy, up.UpdatedBy })
                .Where(id => id != null)
                .Distinct()
                .ToList();

            // Fetch users from Identity context
            var users = await _userContext.AspNetUsers
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName);

            // Fetch user groups
            var groupIds = userPermissions.Select(up => up.UserGroupId).Distinct().ToList();
            var groups = await _context.UserGroups
                .Where(g => groupIds.Contains(g.Id))
                .ToDictionaryAsync(g => g.Id, g => g.UserGroupName);

            // Fetch branch names via branch service
            var result = new List<UserPermissionDto>();
            foreach (var up in userPermissions)
            {
                var branch = await _branchService.GetBranchByIdAsync(up.BranchId);

                result.Add(new UserPermissionDto
                {
                    Id = up.Id,
                    UserId = up.UserId,
                    FullName = users.GetValueOrDefault(up.UserId)!,
                    UserGroupId = up.UserGroupId,
                    UserGroupName = groups.GetValueOrDefault(up.UserGroupId)!,
                    BranchId = up.BranchId,
                    BranchName = branch!.BranchName,
                    RoleName = up.RoleName,
                    CreatedAt = up.CreatedAt,
                    CreatedBy = up.CreatedBy,
                    CreatedByName = users.GetValueOrDefault(up.CreatedBy)!,
                    UpdatedAt = up.UpdatedAt,
                    UpdatedBy = up.UpdatedBy!,
                    UpdatedByName = users!.GetValueOrDefault(up.UpdatedBy)!
                });
            }

            return result!;
        }

        public async Task<UserPermission> GetUserPermissionsByIdAsync(Guid userPermissionId)
        {
            var list = await _context.UserPermissions
                .Where(c => c.Id == userPermissionId).FirstOrDefaultAsync();

            return list!;
        }

        public async Task UpdateUserPermissionAsync(UserPermission entity)
        {
            _context.UserPermissions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
