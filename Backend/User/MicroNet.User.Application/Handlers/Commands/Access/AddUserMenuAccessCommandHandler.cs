using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Access;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Access
{
    public class AddUserMenuAccessCommandHandler : ICommandHandler<AddUserMenuAccessCommand, UserMenuAccess>
    {
        private readonly IUserAccessRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public AddUserMenuAccessCommandHandler(IUserAccessRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<UserMenuAccess> Handle(AddUserMenuAccessCommand request, CancellationToken cancellationToken)
        {
            var entity = request.UserMenuAccess;
            await _repository.AddUserMenuAccess(entity);

            var auditTrail = AuditLog.AddAuditLog(
                entity.AuditInfo.CreatedBy,
                Newtonsoft.Json.JsonConvert.SerializeObject(entity, SerializationHelper.Settings),
                "Created a User Menu Access",
                "User Menu Access"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return entity;
        }
    }
}
