using MicroNet.User.Core.Dto.Permission;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Repositories
{
    public interface IUserPermissionRepository
    {
        Task<UserPermission> AddUserPermissionAsync(UserPermission entity);
        Task<IEnumerable<UserPermissionDto>> GetAllUserPermissionsAsync();
        Task<UserPermission> GetUserPermissionsByIdAsync(Guid userPermissionId);
        Task UpdateUserPermissionAsync(UserPermission entity);
        Task DeleteUserPermissionAsync(Guid userPermissionId);
    }
}
