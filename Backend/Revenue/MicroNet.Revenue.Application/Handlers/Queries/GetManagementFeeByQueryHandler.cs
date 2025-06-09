using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Revenue.Application.Handlers.Queries
{
    public class GetManagementFeeByQueryHandler : IQueryHandler<GetManagementFeeByQuery, ManagementFeeDto>
    {
        private readonly IManagementFeeRepository _repository;

        public GetManagementFeeByQueryHandler(IManagementFeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ManagementFeeDto> Handle(GetManagementFeeByQuery request, CancellationToken cancellationToken)
        {
            var mf = await _repository.GetByIdAsync(request.Id);
            if (mf == null) throw new ManagementFeeIdNotFoundException(request.Id);

            return new ManagementFeeDto
            {
                Id = mf.Id,
                AccountNumber = mf.AccountNumber,
                FeeType = mf.Fee.Type,
                FeeValue = mf.Fee.RateOrAmount,
                CalculatedAmount = mf.CalculatedAmount,
                CreatedBy = mf.AuditInfo.CreatedBy!,
                ChargedAt = mf.ChargedAt
            };
        }
    }
}
