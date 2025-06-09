using MicroNet.Client.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Client.Application.Commands
{
    public record AddClientCommand(ClientDto ClientDto, string CreatedBy) : ICommand<Guid>;
}
