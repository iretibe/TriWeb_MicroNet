using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Core.Dto.Audit;

namespace MicroNet.User.Application.Queries.Audit
{
    public class GetAuditLogByIdQuery : IQuery<AuditLogByIdDto>
    {
        public Guid Id { get; set; }

        public GetAuditLogByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
