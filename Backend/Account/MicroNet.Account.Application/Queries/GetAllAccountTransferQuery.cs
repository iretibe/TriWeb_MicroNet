using MicroNet.Account.Core.Entities;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Queries
{
    public record GetAllAccountTransferQuery : IQuery<List<AccountTransfer>>;
}
