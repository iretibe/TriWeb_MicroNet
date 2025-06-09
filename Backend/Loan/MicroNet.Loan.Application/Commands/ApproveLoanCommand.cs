using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Commands
{
    public record ApproveLoanCommand(Guid RequestId, string ApprovedBy) : ICommand;
}
