using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record UpdateRevenueReversalCommand(Guid Id, RevenueReversalDto ReversalDto, string UpdatedBy) : ICommand;
}
