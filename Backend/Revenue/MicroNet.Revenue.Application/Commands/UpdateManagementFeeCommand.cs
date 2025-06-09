using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record UpdateManagementFeeCommand(Guid Id, ManagementFeeDto FeeDto, string UpdatedBy) : ICommand;
}
