using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Repositories
{
    public interface IUserAccessRepository
    {
        Task<List<MenuEntityDto>> GetMenusForUser(string userId);
        Task<IEnumerable<MenuEntityDto>> GetAllMenuListAsync();
        Task<UserMenuAccess> AddUserMenuAccess(UserMenuAccess entity);
        Task<AssignedMenusDto1> GetAllAssignedSystemMenusByUserIdAsync(string userId);
        Task<UserMenuAccess> AddBulkUserMenuAccessAsync(UserMenuAccess entity);
        Task<AssignedMenusDto1> AssignMenusToUserAsync(AssignedMenusDto1 entity);
        Task<AssignedMenusForUpdateDto> GetUserSystemMenusForUpdateAsync(string userId);
        Task<bool> UpdateAssignMenusToUserAsync(AssignedUserMenusForUpdateDto entity);
    }
}
