using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Policy;
using MicroNet.User.Application.Exceptions.Policy;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.Policy;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Application.Handlers.Commands.Policy
{
    public class UpdatePasswordPolicyCommandHandler : ICommandHandler<UpdatePasswordPolicyCommand, AddUpdPasswordPolicyDto>
    {
        private readonly IPasswordPolicyRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdatePasswordPolicyCommandHandler(IPasswordPolicyRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AddUpdPasswordPolicyDto> Handle(UpdatePasswordPolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = await _repository.GetPasswordPolicyByIdAsync(request.Id);
            if (policy is null)
            {
                throw new PasswordPolicyIdNotFoundException(request.Id);
            }

            var requirements = new PasswordRequirements(policy!.RequiredLength,
                policy.RequireNonAlphanumeric, policy.RequireDigit, policy.RequireLowercase,
                policy.RequireUppercase, policy.RequiredUniqueChars);

            var auditInfo = new AuditInfo(policy.CreatedBy, DateTime.Now, null!, null, null!, null);

            var newPolicy = new PasswordPolicy(policy!.PolicyName, requirements, policy.UserGroupId, auditInfo);
            await _repository.UpdatePasswordPolicyAsync(newPolicy);

            var auditTrail = AuditLog.AddAuditLog(
               policy.CreatedBy,
               Newtonsoft.Json.JsonConvert.SerializeObject(policy, SerializationHelper.Settings),
               "Updated a Password Policy",
               "Password Policy"
           );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return new AddUpdPasswordPolicyDto
            {
                Id = request.Id,
                PolicyName = request.PolicyName,
                RequiredLength = request.RequiredLength,
                RequireNonAlphanumeric = request.RequireNonAlphanumeric,
                RequireDigit = request.RequireDigit,
                RequireLowercase = request.RequireLowercase,
                RequireUppercase = request.RequireUppercase,
                RequiredUniqueChars = request.RequiredUniqueChars,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                UpdatedAt = auditInfo.UpdatedAt,
                UpdatedBy = null!,
                UserGroupId = request.UserGroupId
            };
        }
    }
}
