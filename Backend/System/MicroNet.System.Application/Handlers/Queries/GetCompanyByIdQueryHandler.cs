using MicroNet.Shared.CQRS.Queries;
using MicroNet.System.Application.Exceptions;
using MicroNet.System.Application.Queries;
using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Repositories;

namespace MicroNet.System.Application.Handlers.Queries
{
    public class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, CompanySetup>
    {
        private readonly ICompanySetupRepository _repository;

        public GetCompanyByIdQueryHandler(ICompanySetupRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanySetup> Handle(GetCompanyByIdQuery query, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(query.Id);
            if (company == null)
            {
                throw new CompanyIdNotFoundException(query.Id);
            }

            return company;
        }
    }
}
