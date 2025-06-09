namespace MicroNet.User.Core.Dto.Permission
{
    public class AddUpdUserPermissionDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public Guid BranchId { get; set; }
        public string RoleName { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
    }
}
