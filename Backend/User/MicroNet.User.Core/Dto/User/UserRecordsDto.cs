namespace MicroNet.User.Core.Dto.User
{
    public class UserRecordsDto
    {
        public string Id { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string UserImage { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public string AddedByName { get; set; } = default!;
        public bool Status { get; set; }
        public string StatusName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public bool IsSystemAdmin { get; set; }
        public string RoleName { get; set; } = default!;
    }
}
