using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record CreatePenaltyChargeCommand(CreatePenaltyChargeDto chargedto) : ICommand<Guid>;
}
