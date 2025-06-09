namespace MicroNet.User.Core.Dto.Permission
{
    public class UserPermissionDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = default!;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string CreatedByName { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
        public string UpdatedByName { get; set; } = default!;
    }
}
