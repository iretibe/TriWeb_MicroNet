using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Policy;
using MicroNet.User.Application.Exceptions.Policy;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Policy
{
    public class DeletePasswordPolicyCommandHandler : ICommandHandler<DeletePasswordPolicyCommand>
    {
        private readonly IPasswordPolicyRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeletePasswordPolicyCommandHandler(IPasswordPolicyRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<Unit> Handle(DeletePasswordPolicyCommand request, CancellationToken cancellationToken)
        {
            var policy = await _repository.GetPasswordPolicyByIdAsync(request.Id);
            if (policy is null)
            {
                throw new PasswordPolicyIdNotFoundException(request.Id);
            }

            await _repository.DeletePasswordPolicyAsync(request.Id);

            var auditTrail = AuditLog.AddAuditLog(
                policy.Id.ToString(),
                Newtonsoft.Json.JsonConvert.SerializeObject(policy, SerializationHelper.Settings),
                "Deleted a Password Policy",
                "Password Policy"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return Unit.Value;
        }
    }
}
