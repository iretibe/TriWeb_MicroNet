using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Access;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Access
{
    public class AddBulkUserMenuAccessCommandHandler : ICommandHandler<AddBulkUserMenuAccessCommand, UserMenuAccess>
    {
        private readonly IUserAccessRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public AddBulkUserMenuAccessCommandHandler(IUserAccessRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<UserMenuAccess> Handle(AddBulkUserMenuAccessCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Entity;
            await _repository.AddBulkUserMenuAccessAsync(entity);

            var auditTrail = AuditLog.AddAuditLog(
               entity.AuditInfo.CreatedBy,
               Newtonsoft.Json.JsonConvert.SerializeObject(entity, SerializationHelper.Settings),
               "Created a (Bulk) User Menu Access",
               "User (Bulk) Menu Access"
           );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return entity;
        }
    }
}
