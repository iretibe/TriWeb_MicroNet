using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record UpdateInterestDistributionCommand(Guid Id, InterestDistributionDto DistributionDto, string UpdatedBy) : ICommand;
}
