using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Handlers.Queries
{
    public class GetLoanByIdQueryHandler : IQueryHandler<GetLoanByIdQuery, LoanDto>
    {
        private readonly ILoanRepository _repository;

        public GetLoanByIdQueryHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoanDto> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await _repository.GetByIdAsync(request.Id);
            if (e == null) throw new LoanIdNotFoundException(request.Id);

            return new LoanDto(e.Id, e.LoanCode, e.LoanName, e.MaximumAmount, e.PercentageOfSavings,
                e.InterestRate, e.RepaymentPeriod, e.GracePeriod);
        }
    }
}
