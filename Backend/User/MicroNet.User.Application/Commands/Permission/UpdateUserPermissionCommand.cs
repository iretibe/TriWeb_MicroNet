using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.Permission;

namespace MicroNet.User.Application.Commands.Permission
{
    public class UpdateUserPermissionCommand : ICommand<AddUpdUserPermissionDto>
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public Guid BranchId { get; set; }
        public string RoleName { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
    }
}
