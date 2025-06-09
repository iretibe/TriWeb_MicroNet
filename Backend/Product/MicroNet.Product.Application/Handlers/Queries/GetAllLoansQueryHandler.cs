using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Handlers.Queries
{
    public class GetAllLoansQueryHandler : IQueryHandler<GetAllLoansQuery, List<LoanDto>>
    {
        private readonly ILoanRepository _repository;

        public GetAllLoansQueryHandler(ILoanRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LoanDto>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllAsync();
            return loans.Select(l => new LoanDto(
                l.Id,
                l.LoanCode,
                l.LoanName,
                l.MaximumAmount,
                l.PercentageOfSavings,
                l.InterestRate,
                l.RepaymentPeriod,
                l.GracePeriod)).ToList();
        }
    }
}
