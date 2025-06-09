using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Commands
{
    public record CreateManagementFeeCommand(CreateManagementFeeDto managementFeeDto) : ICommand<Guid>;
}
