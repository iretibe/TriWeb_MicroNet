using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.Policy;

namespace MicroNet.User.Application.Commands.Policy
{
    public class AddPasswordPolicyCommand : ICommand<AddUpdPasswordPolicyDto>
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
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; } = default!;
        public Guid UserGroupId { get; set; }
    }
}
