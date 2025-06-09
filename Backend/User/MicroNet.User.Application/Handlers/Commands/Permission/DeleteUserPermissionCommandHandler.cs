using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Permission;
using MicroNet.User.Application.Exceptions.Permission;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Permission
{
    public class DeleteUserPermissionCommandHandler : ICommandHandler<DeleteUserPermissionCommand>
    {
        private readonly IUserPermissionRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public DeleteUserPermissionCommandHandler(IUserPermissionRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<Unit> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _repository.GetUserPermissionsByIdAsync(request.Id);
            if (permission is null)
            {
                throw new UserPermissionIdNotFoundException(request.Id);
            }

            await _repository.DeleteUserPermissionAsync(request.Id);

            var auditTrail = AuditLog.AddAuditLog(
                permission.Id.ToString(),
                Newtonsoft.Json.JsonConvert.SerializeObject(permission, SerializationHelper.Settings),
                "Deleted a User Permission",
                "User Permission"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return Unit.Value;
        }
    }
}
