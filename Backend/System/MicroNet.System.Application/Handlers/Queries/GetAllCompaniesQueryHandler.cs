using MicroNet.Shared.CQRS.Queries;
using MicroNet.System.Application.Queries;
using MicroNet.System.Core.Entities;
using MicroNet.System.Core.Repositories;

namespace MicroNet.System.Application.Handlers.Queries
{
    public class GetAllCompaniesQueryHandler : IQueryHandler<GetAllCompaniesQuery, CompanySetup[]>
    {
        private readonly ICompanySetupRepository _repository;

        public GetAllCompaniesQueryHandler(ICompanySetupRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanySetup[]> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _repository.GetAllAsync();

            if (companies == null || !companies.Any())
            {
                return Array.Empty<CompanySetup>();
            }

            return companies.ToArray();
        }
    }
}
