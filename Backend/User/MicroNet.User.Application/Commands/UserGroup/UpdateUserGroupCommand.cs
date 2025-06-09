using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.UserGroup;

namespace MicroNet.User.Application.Commands.UserGroup
{
    public class UpdateUserGroupCommand : ICommand<AddUserGroupDto>
    {
        public Guid Id { get; set; }
        public string UserGroupName { get; set; } = default!;
        public bool IsActive { get; set; } = false;
        public Guid BranchId { get; set; }
        public string StartTime { get; set; } = default!;
        public string EndTime { get; set; } = default!;
        public List<string> WorkingDays { get; set; } = default!;
        public List<AddMenuDto> Menus { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = default!;
    }
}
