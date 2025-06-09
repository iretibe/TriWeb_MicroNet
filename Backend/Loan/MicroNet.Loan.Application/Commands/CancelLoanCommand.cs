using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Commands
{
    public record CancelLoanCommand(Guid RequestId, string CancelledBy, string Reason) : ICommand;
}
