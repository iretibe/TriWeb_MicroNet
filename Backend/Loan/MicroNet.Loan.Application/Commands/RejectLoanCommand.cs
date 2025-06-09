using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Commands
{
    public record RejectLoanCommand(Guid RequestId, string RejectedBy, string Reason) : ICommand;
}
