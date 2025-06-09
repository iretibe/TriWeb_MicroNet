using MicroNet.Shared.CQRS.Queries;
using MicroNet.User.Application.Exceptions;
using MicroNet.User.Application.Exceptions.Audit;
using MicroNet.User.Application.Queries.Audit;
using MicroNet.User.Core.Dto.Audit;
using MicroNet.User.Core.Entities;
using MicroNet.User.Core.Repositories;

namespace MicroNet.User.Application.Handlers.Queries.Audit
{
    public class GetAuditLogByIdQueryHandler : IQueryHandler<GetAuditLogByIdQuery, AuditLogByIdDto>
    {
        private readonly IAuditLogRepository _repository;

        public GetAuditLogByIdQueryHandler(IAuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuditLogByIdDto> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
        {
            var audit = await _repository.GetAuditLogByIdAsync(new AggregateId(request.Id));

            if (audit == null)
                throw new AuditLogNotFoundException(request.Id);

            return new AuditLogByIdDto
            {
                Id = audit.Id,
                AuditDate = audit.AuditDate,
                UserId = audit.UserId,
                FullName = audit.FullName,
                Data = audit.Data,
                Method = audit.Method,
                EntityType = audit.EntityType
            };
        }
    }
}
