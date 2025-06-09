using MicroNet.Shared.CQRS.Commands;
using MicroNet.Sundry.Core.Dtos;

namespace MicroNet.Sundry.Application.Commands
{
    public record CreateAccountDefinitionCommand(AccountingDto Account) : ICommand<Guid>;
}
