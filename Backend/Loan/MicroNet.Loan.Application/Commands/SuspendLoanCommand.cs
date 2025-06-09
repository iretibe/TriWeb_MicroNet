using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Commands
{
    public record SuspendLoanCommand(Guid RequestId, string SuspendedBy, string Reason, int DurationDays) : ICommand;
}
