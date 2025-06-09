using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Commands
{
    public record TransferAccountCommand(string AccountNumber, string AccountName, string CurrentBranch, decimal Balance, string ToBranchName, string TransferredBy) : ICommand<Guid>;
}
