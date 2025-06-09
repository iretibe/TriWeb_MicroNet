using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Dto.User;

namespace MicroNet.User.Application.Commands.User
{
    public class UpdateUserCommand : ICommand<UserDto>
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string PhysicalAddress { get; set; } = default!;
        public string PostalAddress { get; set; } = default!;
        public string UserImage { get; set; } = default!;
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public bool IsSystemAdmin { get; set; }
    }
}
