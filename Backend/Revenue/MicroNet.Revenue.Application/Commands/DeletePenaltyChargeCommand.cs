using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record DeletePenaltyChargeCommand(Guid Id) : ICommand;
}
