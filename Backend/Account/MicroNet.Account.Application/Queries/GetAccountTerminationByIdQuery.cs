using MicroNet.Account.Core.Entities;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Account.Application.Queries
{
    public record GetAccountTerminationByIdQuery(Guid Id) : IQuery<AccountTermination>;
}
