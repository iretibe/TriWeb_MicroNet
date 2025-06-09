using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.UserGroup;
using MicroNet.User.Application.Exceptions.UserGroup;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.UserGroup
{
    public class DeleteUserGroupCommandHandler : ICommandHandler<DeleteUserGroupCommand>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeleteUserGroupCommandHandler(IUserGroupRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<Unit> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetUserGroupByIdAsync(request.Id);
            if (group is null)
            {
                throw new UserGroupIdNotFoundException(request.Id);
            }

            await _repository.DeleteUserGroupAsync(request.Id);

            var auditTrail = AuditLog.AddAuditLog(
                group.Id.ToString(),
                Newtonsoft.Json.JsonConvert.SerializeObject(group, SerializationHelper.Settings),
                "Deleted a User Group",
                "User Group"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return Unit.Value;
        }
    }
}
