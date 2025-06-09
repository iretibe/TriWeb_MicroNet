using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Permission;
using MicroNet.User.Application.Exceptions.Permission;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.Permission;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Application.Handlers.Commands.Permission
{
    public class UpdateUserPermissionCommandHandler : ICommandHandler<UpdateUserPermissionCommand, AddUpdUserPermissionDto>
    {
        private readonly IUserPermissionRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdateUserPermissionCommandHandler(IUserPermissionRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AddUpdUserPermissionDto> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _repository.GetUserPermissionsByIdAsync(request.Id);
            if (permission is null)
            {
                throw new UserPermissionIdNotFoundException(request.Id);
            }

            var auditInfo = new AuditInfo(permission.AuditInfo.CreatedBy, DateTime.Now, null!, null, null!, null);

            var newPermission = new UserPermission(permission.AuditInfo.CreatedBy, permission.UserGroupId, permission.BranchId, permission.RoleName, auditInfo);
            await _repository.UpdateUserPermissionAsync(newPermission);

            var auditTrail = AuditLog.AddAuditLog(
               permission.AuditInfo.CreatedBy,
               Newtonsoft.Json.JsonConvert.SerializeObject(permission, SerializationHelper.Settings),
               "Updated a User Permission",
               "User Permission"
           );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return new AddUpdUserPermissionDto
            {
                Id = request.Id,
                UserId = request.UserId,
                UserGroupId = request.UserGroupId,
                BranchId = request.BranchId,
                RoleName = request.RoleName,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy,
                UpdatedAt = auditInfo.UpdatedAt,
                UpdatedBy = null!
            };
        }
    }
}
