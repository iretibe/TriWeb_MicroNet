using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Application.Commands.Access
{
    public class AddUserMenuAccessCommand : ICommand<UserMenuAccess>
    {
        public UserMenuAccess UserMenuAccess { get; set; }

        public AddUserMenuAccessCommand(UserMenuAccess userMenuAccess)
        {
            UserMenuAccess = userMenuAccess;
        }
    }
}
