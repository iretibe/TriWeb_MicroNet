using MicroNet.User.Core.Dto.User;
using MicroNet.User.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace MicroNet.User.Core.Repositories
{
    public interface IUserRepository
    {
        Task<AspNetUsers> GetUserByIdAsync(string id);
        Task<AspNetUsers> GetUserByNameAsync(string name);
        Task<AspNetUsers> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserRecordsDto>> GetAllUsersAsync();
        Task<IEnumerable<SysAdminRecordsDto>> GetAllSysAdminUsersAsync();
        Task UpdateUserAsync(AspNetUsers user);
        Task UpdateUserWithRoleAsync(string userId, string roleName);
        Task DeleteUserAsync(string userId);
        Task<IdentityResult> AddRoleToUsersAsync(string userId, string roleName);
        Task<IEnumerable<GetAllSysAdministratorsDto>> GetAllSysAdministrators();
        Task UpdateUserStatusById(bool Status, string UserId);
        Task UpdateIsFirstTimeLogin(bool IsFirstTimeLogin, string UserId);
    }
}
