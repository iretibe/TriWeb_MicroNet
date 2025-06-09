using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Access;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Access
{
    public class UpdateAssignMenusToUserCommandHandler : ICommandHandler<UpdateAssignMenusToUserCommand, bool>
    {
        private readonly IUserAccessRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdateAssignMenusToUserCommandHandler(IUserAccessRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<bool> Handle(UpdateAssignMenusToUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.Entity;
            await _repository.UpdateAssignMenusToUserAsync(entity);

            var response = new AssignedUserMenusForUpdateDto
            {
                UserId = entity.UserId,
                AssignedMenus = entity.AssignedMenus
            };

            var auditTrail = AuditLog.AddAuditLog(
                "1A9C243D-1228-4B83-AC01-D41BB8CC5541",
                Newtonsoft.Json.JsonConvert.SerializeObject(entity, SerializationHelper.Settings),
                "Updated User Menu Assignment",
                "User Menu Assignment"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return true;
        }
    }
}
