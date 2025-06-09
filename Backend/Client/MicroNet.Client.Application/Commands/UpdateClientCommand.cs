using MicroNet.Client.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Client.Application.Commands
{
    public record UpdateClientCommand(Guid Id, ClientDto ClientDto, string UpdatedBy) : ICommand;
}
