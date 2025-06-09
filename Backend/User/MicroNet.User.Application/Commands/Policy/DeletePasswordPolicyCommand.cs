using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.User.Application.Commands.Policy
{
    public class DeletePasswordPolicyCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeletePasswordPolicyCommand(Guid id)
        {
            Id = id;
        }
    }
}
