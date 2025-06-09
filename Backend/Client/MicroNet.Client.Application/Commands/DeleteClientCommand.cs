using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Client.Application.Commands
{
    public record DeleteClientCommand(Guid Id) : ICommand;
}
