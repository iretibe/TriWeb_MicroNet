namespace MicroNet.User.Core.Dto.User
{
    public class SysAdminRecordsDto
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public bool Status { get; set; }
        public string StatusName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = default!;
        public bool IsSystemAdmin { get; set; }
        public string RoleName { get; set; } = default!;
    }
}
