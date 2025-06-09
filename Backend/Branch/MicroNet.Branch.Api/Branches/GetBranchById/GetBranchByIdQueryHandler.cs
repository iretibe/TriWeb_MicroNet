using MicroNet.Branch.Api.Dtos;
using MicroNet.Branch.Api.Exceptions;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.Branches.GetBranchById
{
    public class GetBranchByIdQueryHandler : IQueryHandler<GetBranchByIdQuery, BranchDto>
    {
        private readonly IBranchRepository _repository;

        public GetBranchByIdQueryHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<BranchDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var b = await _repository.GetBranchByIdAsync(request.BranchId);

            if (b == null)
            {
                throw new BranchNotFoundException(request.BranchId);
            }

            return new BranchDto
            {
                Id = b.Id,
                BranchCode = b.BranchCode,
                BranchName = b.BranchName,
                Address = new AddressDto
                {
                    PhysicalAddress = b.PhysicalAddress.PostalAddress!
                },
                Region = b.Region,
                SetupDate = b.SetupDate,
                BranchManagerName = b.ManagerName,
                ProductSummary = b.ProductSummaries.Select(ps => new ProductSummaryDto
                {
                    Loan = new LoanSummaryDto
                    {
                        //Id = ps.Loan.Id,
                        //BranchId = ps.Loan.BranchId,
                        NumberOfLoans = ps.NumberOfLoans,
                        TotalLoanAmount = ps.TotalLoanAmount,
                        TotalInterest = ps.TotalInterest,
                        TotalRepayment = ps.TotalRepayment,
                        ProcessingFees = ps.ProcessingFees,
                        PenaltyCharges = ps.PenaltyCharges,
                        TotalLoanBalance = ps.TotalLoanBalance
                    },
                    OtherProduct = new OtherProductSummaryDto
                    {
                        ProductAmount = ps.ProductAmount,
                        Interest = ps.Interest,
                        Withdrawal = ps.Withdrawal,
                        ManagementFees = ps.ManagementFees,
                        Balance = ps.Balance
                    }
                }).ToList(),
            };
        }
    }
}
