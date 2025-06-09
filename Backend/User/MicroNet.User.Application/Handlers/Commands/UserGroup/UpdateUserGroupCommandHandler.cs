using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.UserGroup;
using MicroNet.User.Application.Exceptions.UserGroup;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;
using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Application.Handlers.Commands.UserGroup
{
    public class UpdateUserGroupCommandHandler : ICommandHandler<UpdateUserGroupCommand, AddUserGroupDto>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UpdateUserGroupCommandHandler(IUserGroupRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AddUserGroupDto> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetUserGroupByIdAsync(request.Id);
            if (group is null)
            {
                throw new UserGroupIdNotFoundException(request.Id);
            }

            var auditInfo = new AuditInfo(null!, null, request.CreatedBy, DateTime.UtcNow, null!, null);

            // Convert StartTime and EndTime from string to TimeSpan
            if (!TimeSpan.TryParse(request.StartTime, out var startTime))
            {
                throw new InvalidTimespanException(request.StartTime);
            }

            if (!TimeSpan.TryParse(request.EndTime, out var endTime))
            {
                throw new InvalidTimespanException(request.EndTime);
            }

            var workingHours = new TimeRange(startTime, endTime);

            var newGroup = new Core.Entities.UserGroup(group.UserGroupName, group.BranchId, workingHours, auditInfo);
            await _repository.UpdateUserGroupAsync(newGroup);

            var auditTrail = AuditLog.AddAuditLog(
               request.CreatedBy,
               Newtonsoft.Json.JsonConvert.SerializeObject(group, SerializationHelper.Settings),
               "Updated a User Group",
               "User Group"
           );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return new AddUserGroupDto
            {
                Id = request.Id,
                UserGroupName = request.UserGroupName,
                IsActive = request.IsActive,
                BranchId = request.BranchId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                WorkingDays = request.WorkingDays,
                Menus = request.Menus,
                CreatedAt = DateTime.Now,
                CreatedBy = request.CreatedBy
            };
        }
    }
}
