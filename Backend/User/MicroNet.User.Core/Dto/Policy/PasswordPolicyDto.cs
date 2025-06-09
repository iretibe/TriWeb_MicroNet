namespace MicroNet.User.Core.Dto.Policy
{
    public class PasswordPolicyDto
    {
        public Guid Id { get; set; }
        public string PolicyName { get; set; } = default!;
        public int RequiredLength { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequiredUniqueChars { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string CreatedByFullName { get; set; } = default!;
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
        public string UpdatedByFullName { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public string UserGroupName { get; set; } = default!;
    }
}
