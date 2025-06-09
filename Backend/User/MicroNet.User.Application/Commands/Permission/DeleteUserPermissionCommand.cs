using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.User.Application.Commands.Permission
{
    public class DeleteUserPermissionCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteUserPermissionCommand(Guid id)
        {
            Id = id;
        }
    }
}
