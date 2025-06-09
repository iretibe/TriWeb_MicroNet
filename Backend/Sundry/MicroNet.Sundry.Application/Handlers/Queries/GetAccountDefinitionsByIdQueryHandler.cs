using MicroNet.Shared.CQRS.Queries;
using MicroNet.Sundry.Application.Exceptions;
using MicroNet.Sundry.Application.Queries;
using MicroNet.Sundry.Core.Dtos;
using MicroNet.Sundry.Core.Repositories;

namespace MicroNet.Sundry.Application.Handlers.Queries
{
    public class GetAccountDefinitionsByIdQueryHandler : IQueryHandler<GetAccountDefinitionsByIdQuery, AccountingDto>
    {
        private readonly IAccountingRepository _repository;

        public GetAccountDefinitionsByIdQueryHandler(IAccountingRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountingDto> Handle(GetAccountDefinitionsByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.GetByIdAsync(request.id);

            if (account == null) 
                throw new AccountingIdNotFoundException(request.id);

            return new AccountingDto
            {
                Id = account.Id,
                Code = account.Code,
                Name = account.Name,
                Category = account.Category.ToString(),
                CreatedBy = account.AuditInfo.CreatedBy!,
                CreatedAt = account.AuditInfo.CreatedAt ?? DateTime.MinValue
            };
        }
    }
}
