using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.UserGroup;
using MicroNet.User.Application.Exceptions.UserGroup;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.UserGroup
{
    public class UpdateUserGroupMenusCommandHandler : ICommandHandler<UpdateUserGroupMenusCommand, UpdateUserGroupDto>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdateUserGroupMenusCommandHandler(IUserGroupRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<UpdateUserGroupDto> Handle(UpdateUserGroupMenusCommand request, CancellationToken cancellationToken)
        {
            var updated = await _repository.UpdateUserGroupMenusAsync(request.Entity);

            if (!updated)
            {
                throw new UserGroupIdNotFoundException(request.Entity.Id);
            }

            // Audit log entry
            var auditTrail = AuditLog.AddAuditLog(
                request.Entity.UpdatedBy,
                Newtonsoft.Json.JsonConvert.SerializeObject(request.Entity, SerializationHelper.Settings),
                $"Updated User Group Menu: {request.Entity.UserGroupName}",
                "User Group"
            );

            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return request.Entity;
        }
    }
}
