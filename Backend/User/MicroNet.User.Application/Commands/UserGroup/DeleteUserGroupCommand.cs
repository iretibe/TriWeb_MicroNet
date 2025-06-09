using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.User.Application.Commands.UserGroup
{
    public class DeleteUserGroupCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteUserGroupCommand(Guid id)
        {
            Id = id;
        }
    }
}
