using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Queries.Audit;
using MicroNet.User.Core.Dto.Audit;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Audit
{
    public class GetAllAuditLogsQueryHandler : IQueryHandler<GetAllAuditLogsQuery, IEnumerable<AuditLogDto>>
    {
        private readonly IAuditLogRepository _repository;

        public GetAllAuditLogsQueryHandler(IAuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuditLogDto>> Handle(GetAllAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var audits = await _repository.GetAllAuditLogsAsync();

            var result = audits.Select(d => new AuditLogDto
            {
                Id = d.Id,
                AuditDate = d.AuditDate,
                UserId = d.UserId,
                FullName = d.FullName,
                Data = d.Data,
                Method = d.Method,
                EntityType = d.EntityType
            });

            return result;
        }
    }
}
