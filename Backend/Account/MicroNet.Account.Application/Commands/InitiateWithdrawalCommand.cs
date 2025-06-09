using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Commands
{
    public record InitiateWithdrawalCommand(string AccountNumber, decimal Amount, string Reference, string PaymentMode, string RequestedBy) : ICommand<Guid>;
}
