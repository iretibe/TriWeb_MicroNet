using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record DeleteInterestDistributionCommand(Guid Id) : ICommand;
}
