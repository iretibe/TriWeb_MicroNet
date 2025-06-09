using MicroNet.User.Core.Dto.Policy;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Models;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Infrastructure.Repositories
{
    public class PasswordPolicyRepository : IPasswordPolicyRepository
    {
        private readonly UserContext _context;
        private readonly IdentityUserContext _userContext;

        public PasswordPolicyRepository(UserContext context, IdentityUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<PasswordPolicy> AddPasswordPolicyAsync(PasswordPolicy entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> CheckPasswordPolicyNameExistsAsync(string PasswordPolicyName)
        {
            return await _context.PasswordPolicies.AnyAsync(pp => pp.PolicyName == PasswordPolicyName);
        }

        public async Task DeletePasswordPolicyAsync(Guid PasswordPolicyId)
        {
            var model = await _context.PasswordPolicies.SingleOrDefaultAsync(c => c.Id == PasswordPolicyId);
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PasswordPolicyDto>> GetAllPasswordPoliciesAsync()
        {
            var query = await (from pp in _context.PasswordPolicies
                        let createdByUser = _userContext.AspNetUsers.FirstOrDefault(u => u.Id == pp.AuditInfo.CreatedBy)
                        let updatedByUser = _userContext.AspNetUsers.FirstOrDefault(u => u.Id == pp.AuditInfo.UpdatedBy)
                        let userGroup = _context.UserGroups.FirstOrDefault(ug => ug.Id == pp.UserGroupId)
                        select new PasswordPolicyDto
                        {
                            Id = pp.Id,
                            PolicyName = pp.PolicyName,
                            RequiredLength = pp.Requirements.RequiredLength,
                            RequireNonAlphanumeric = pp.Requirements.RequireNonAlphanumeric,
                            RequireDigit = pp.Requirements.RequireDigit,
                            RequireLowercase = pp.Requirements.RequireLowercase,
                            RequireUppercase = pp.Requirements.RequireUppercase,
                            RequiredUniqueChars = pp.Requirements.RequireUniqueChars,
                            CreatedAt = pp.AuditInfo.CreatedAt,
                            CreatedBy = pp.AuditInfo.CreatedBy,
                            CreatedByFullName = createdByUser != null ? createdByUser.FullName : null!,
                            UpdatedAt = pp.AuditInfo.UpdatedAt,
                            UpdatedBy = pp.AuditInfo.UpdatedBy!,
                            UpdatedByFullName = updatedByUser != null ? updatedByUser.FullName : null!,
                            UserGroupId = pp.UserGroupId,
                            UserGroupName = userGroup != null ? userGroup.UserGroupName : null!
                        }).ToListAsync();

            return query;
        }

        public async Task<PasswordPolicyDto> GetPasswordPolicyByIdAsync(Guid passwordPolicyId)
        {
            var query = await (from pp in _context.PasswordPolicies
                               where pp.Id == passwordPolicyId
                               let createdByUser = _userContext.AspNetUsers.FirstOrDefault(u => u.Id == pp.AuditInfo.CreatedBy)
                               let updatedByUser = _userContext.AspNetUsers.FirstOrDefault(u => u.Id == pp.AuditInfo.UpdatedBy)
                               let userGroup = _context.UserGroups.FirstOrDefault(ug => ug.Id == pp.UserGroupId)
                               select new PasswordPolicyDto
                               {
                                   Id = pp.Id,
                                   PolicyName = pp.PolicyName,
                                   RequiredLength = pp.Requirements.RequiredLength,
                                   RequireNonAlphanumeric = pp.Requirements.RequireNonAlphanumeric,
                                   RequireDigit = pp.Requirements.RequireDigit,
                                   RequireLowercase = pp.Requirements.RequireLowercase,
                                   RequireUppercase = pp.Requirements.RequireUppercase,
                                   RequiredUniqueChars = pp.Requirements.RequireUniqueChars,
                                   CreatedAt = pp.AuditInfo.CreatedAt,
                                   CreatedBy = pp.AuditInfo.CreatedBy,
                                   CreatedByFullName = createdByUser != null ? createdByUser.FullName : null!,
                                   UpdatedAt = pp.AuditInfo.UpdatedAt,
                                   UpdatedBy = pp.AuditInfo.UpdatedBy!,
                                   UpdatedByFullName = updatedByUser != null ? updatedByUser.FullName : null!,
                                   UserGroupId = pp.UserGroupId,
                                   UserGroupName = userGroup != null ? userGroup.UserGroupName : null!
                               }).FirstOrDefaultAsync();

            return query!;
        }

        public async Task UpdatePasswordPolicyAsync(PasswordPolicy entity)
        {
            _context.PasswordPolicies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
