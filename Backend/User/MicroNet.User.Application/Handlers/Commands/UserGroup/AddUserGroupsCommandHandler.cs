using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Application.Commands.UserGroup;
using MicroNet.User.Application.Exceptions.UserGroup;
using MicroNet.User.Application.Helpers;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Commands.UserGroup
{
    public class AddUserGroupsCommandHandler : ICommandHandler<AddUserGroupsCommand, AddUserGroupDto>
    {
        private readonly IUserGroupRepository _repository;
        private readonly IAuditLogRepository _auditLogRepository;

        public AddUserGroupsCommandHandler(IUserGroupRepository repository,
            IAuditLogRepository auditLogRepository)
        {
            _repository = repository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AddUserGroupDto> Handle(AddUserGroupsCommand request, CancellationToken cancellationToken)
        {
            var group = await _repository.GetUserGroupByIdAsync(request.Id);
            if (group is not null)
            {
                throw new UserGroupAlreadyExistsException(request.Id);
            }

            var existing = await _repository.GetUserGroupByNameAsync(request.UserGroupName, request.BranchId);
            if (existing is not null)
                throw new UserGroupNameAlreadyExistsException(request.UserGroupName);

            var resultDto = await _repository.AddUserGroupsAsync(new AddUserGroupDto
            {
                Id = request.Id,
                UserGroupName = request.UserGroupName,
                IsActive = request.IsActive,
                BranchId = request.BranchId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                WorkingDays = request.WorkingDays,
                Menus = request.Menus,
                CreatedBy = request.CreatedBy
            });

            var auditTrail = AuditLog.AddAuditLog(
                request.CreatedBy,
                Newtonsoft.Json.JsonConvert.SerializeObject(resultDto, SerializationHelper.Settings),
                "Created a User Group",
                "User Group"
            );
            await _auditLogRepository.AddAuditLogAsync(auditTrail);

            return resultDto;
        }
    }
}
