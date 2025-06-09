using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    internal class GetAllManagementFeeQueryHandler : IQueryHandler<GetAllManagementFeeQuery, List<ManagementFeeDto>>
    {
        private readonly IManagementFeeRepository _repository;

        public GetAllManagementFeeQueryHandler(IManagementFeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ManagementFeeDto>> Handle(GetAllManagementFeeQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllAsync();
            return loans.Select(mf => new ManagementFeeDto
            {
                Id = mf.Id,
                AccountNumber = mf.AccountNumber,
                FeeType = mf.Fee.Type,
                FeeValue = mf.Fee.RateOrAmount,
                CalculatedAmount = mf.CalculatedAmount,
                CreatedBy = mf.AuditInfo.CreatedBy!,
                ChargedAt = mf.ChargedAt
            }).ToList();
        }
    }
}
