using MicroNet.Shared.CQRS.Commands;
using MicroNet.User.Core.Entities;

namespace MicroNet.User.Application.Commands.Access
{
    public class AddBulkUserMenuAccessCommand : ICommand<UserMenuAccess>
    {
        public UserMenuAccess Entity { get; set; }

        public AddBulkUserMenuAccessCommand(UserMenuAccess entity)
        {
            Entity = entity;
        }
    }
}
