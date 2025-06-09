using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Audit;

namespace MicroNet.User.Application.Queries.Audit
{
    public class GetAllAuditLogsQuery : IQuery<IEnumerable<AuditLogDto>>
    {
    }
}
