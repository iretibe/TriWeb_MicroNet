using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.Access;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.Access
{
    public class AssignMenusToUserCommandHandler : ICommandHandler<AssignMenusToUserCommand, AssignedMenusDto1>
    {
        private readonly IUserAccessRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public AssignMenusToUserCommandHandler(IUserAccessRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AssignedMenusDto1> Handle(AssignMenusToUserCommand request, CancellationToken cancellationToken)
        {
            var entity = request.AssignedMenus;
            await _repository.AssignMenusToUserAsync(entity);

            var auditTrail = AuditLog.AddAuditLog(
                entity.CreatedBy,
                Newtonsoft.Json.JsonConvert.SerializeObject(entity, SerializationHelper.Settings),
                "Created a Menu Assignment To User",
                "User Menu Assignment"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return entity;
        }
    }
}
