using MicroNet.Branch.Api.Dtos;
using MicroNet.Branch.Api.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Branch.Api.Branches.GetAllBranches
{
    public class GetAllBranchesQueryHandler : IQueryHandler<GetAllBranchesQuery, IEnumerable<BranchDto>>
    {
        private readonly IBranchRepository _repository;

        public GetAllBranchesQueryHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BranchDto>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _repository.GetAllBranchesAsync();

            return branches.Select(b => new BranchDto
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
            }).ToList();
        }
    }
}
