using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.User.Application.Commands.User
{
    public class DeleteUserCommand : ICommand
    {
        public string Id { get; set; }

        public DeleteUserCommand(string id)
        {
            Id = id;
        }
    }
}
