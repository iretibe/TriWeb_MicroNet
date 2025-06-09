using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record DeleteRevenueReversalCommand(Guid Id) : ICommand;
}
