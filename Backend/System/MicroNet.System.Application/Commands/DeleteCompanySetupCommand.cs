using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.System.Application.Commands
{
    public class DeleteCompanySetupCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteCompanySetupCommand(Guid id)
        {
            Id = id;
        }
    }
}
