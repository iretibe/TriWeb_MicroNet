namespace MicroNet.User.Core.Dto.UserGroup
{
    public class UserGroupDto
    {
        public Guid Id { get; set; }
        public string UserGroupName { get; set; } = default!;
        public bool IsActive { get; set; } // For inactive mode setup
        public Guid BranchId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string BranchName { get; set; } = default!;
    }
}
