namespace MicroNet.User.Core.Dto.User
{
    public class GetAllSysAdministratorsDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string UserGroupName { get; set; } = default!;
    }
}
