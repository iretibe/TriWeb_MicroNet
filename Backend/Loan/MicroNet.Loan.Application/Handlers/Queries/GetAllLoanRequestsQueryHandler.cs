using MicroNet.Loan.Application.Queries;
using MicroNet.Loan.Core.Dtos;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Loan.Application.Handlers.Queries
{
    public class GetAllLoanRequestsQueryHandler : IQueryHandler<GetAllLoanRequestsQuery, IEnumerable<LoanRequestDto>>
    {
        private readonly ILoanRequestRepository _repository;

        public GetAllLoanRequestsQueryHandler(ILoanRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LoanRequestDto>> Handle(GetAllLoanRequestsQuery request, CancellationToken cancellationToken)
        {
            var loans = await _repository.GetAllAsync();

            return loans.Select(loan => new LoanRequestDto
            {
                Id = loan.Id,
                ClientAccountNumber = loan.AccountNumber,
                ClientAccountName = loan.ClientName,
                BranchName = loan.Branch,
                LoanType = loan.LoanType,
                InterestRate = loan.InterestRate,
                RepaymentPeriod = loan.RepaymentPeriod,
                RequestedAmount = loan.MaximumAmount,
                RequestedPrincipal = loan.RequestedPrincipal,
                RiskMargin = loan.RiskMargin,
                InsuranceAmount = loan.InsuranceAmount,
                DisbursementMedium = loan.DisbursementMedium,
                Status = loan.Status.ToString(),
                RequestedBy = loan.AuditInfo.CreatedBy!,
                RequestedAt = (DateTime)loan.AuditInfo.CreatedAt!,
                SupportingDocuments = loan.SupportingDocuments.Select(ld => new LoanDocumentDto
                {
                    FileName = ld.FileName,
                    FileUrl = ld.FileUrl
                }).ToList()
            }).ToList();
        }
    }
}
