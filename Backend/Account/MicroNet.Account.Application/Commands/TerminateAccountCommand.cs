using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Account.Application.Commands
{
    public record TerminateAccountCommand(string AccountNumber, string Reason, string TerminatedBy) : ICommand<Guid>;
}
