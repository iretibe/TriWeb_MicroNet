using MicroNet.User.Core.Dto.Policy;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Core.Repositories
{
    public interface IPasswordPolicyRepository
    {
        Task<PasswordPolicy> AddPasswordPolicyAsync(PasswordPolicy entity);
        Task<IEnumerable<PasswordPolicyDto>> GetAllPasswordPoliciesAsync();
        Task<PasswordPolicyDto> GetPasswordPolicyByIdAsync(Guid passwordPolicyId);
        Task UpdatePasswordPolicyAsync(PasswordPolicy entity);
        Task DeletePasswordPolicyAsync(Guid PasswordPolicyId);
        Task<bool> CheckPasswordPolicyNameExistsAsync(string PasswordPolicyName);
    }
}
