using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Repositories
{
    public interface IUserGroupRepository
    {
        Task<AddUserGroupDto> AddUserGroupsAsync(AddUserGroupDto entity);
        Task<IEnumerable<UserGroupDto>> GetAllUserGroupsAsync();
        Task<GetUserGroupDto> GetUserGroupByIdAsync(Guid userGroupId);
        Task<IEnumerable<UserGroupMenu>> GetUserGroupMenuByUniqueIdAsync(Guid userGroupId);
        Task<UserGroup> GetUserGroupByNameAsync(string userGroupName, Guid branchId);
        Task UpdateUserGroupAsync(UserGroup entity);
        Task DeleteUserGroupAsync(Guid userGroupId);
        Task<bool> CheckUserGroupNameExistsAsync(string userGroupName);
        Task AddUserRoles(string Id, string RoleName, string NormalizedName);
        Task<List<GetWorkingDayDto>> GetWorkingDaysByUserGroupIdAsync(Guid userGroupId);
        Task<List<GetMenuDto>> GetMenusByUserGroupIdAsync(Guid userGroupId);
        Task<List<GetSubMenuDto>> GetSubMenusByMenuIdAsync(Guid userGroupId, Guid menuId);
        Task<bool> UpdateUserGroupMenusAsync(UpdateUserGroupDto entity);
    }
}
