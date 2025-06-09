using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record UpdatePenaltyChargeCommand(Guid Id, PenaltyChargeDto ChargeDto, string UpdatedBy) : ICommand;
}
