using MicroNet.Shared.CQRS.Queries;
using MicroNet.Sundry.Core.Dtos;

namespace MicroNet.Sundry.Application.Queries
{
    public record GetAllAccountDefinitionsQuery() : IQuery<IEnumerable<AccountingDto>>;
}
