using MicroNet.Shared.CQRS.Queries;
using MicroNet.Sundry.Application.Queries;
using MicroNet.Sundry.Core.Dtos;
using MicroNet.Sundry.Core.Enums;
using MicroNet.Sundry.Core.Repositories;

namespace MicroNet.Sundry.Application.Handlers.Queries
{
    public class GetAllAccountDefinitionsQueryHandler : IQueryHandler<GetAllAccountDefinitionsQuery, IEnumerable<AccountingDto>>
    {
        private readonly IAccountingRepository _repository;

        public GetAllAccountDefinitionsQueryHandler(IAccountingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AccountingDto>> Handle(GetAllAccountDefinitionsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _repository.GetAllAsync();

            return accounts.Select(x => new AccountingDto
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Category = x.Category.ToString(),
                CreatedBy = x.AuditInfo.CreatedBy!,
                CreatedAt = x.AuditInfo.CreatedAt ?? DateTime.MinValue
            });
        }
    }

}
