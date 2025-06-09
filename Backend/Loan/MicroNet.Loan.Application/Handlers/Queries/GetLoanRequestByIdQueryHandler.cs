using MicroNet.Loan.Application.Exceptions;
using MicroNet.Loan.Application.Queries;
using MicroNet.Loan.Core.Dtos;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Loan.Application.Handlers.Queries
{
    public class GetLoanRequestByIdQueryHandler : IQueryHandler<GetLoanRequestByIdQuery, LoanRequestDto>
    {
        private readonly ILoanRequestRepository _repository;

        public GetLoanRequestByIdQueryHandler(ILoanRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoanRequestDto> Handle(GetLoanRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(request.RequestId);

            if (loan == null)
                throw new LoanRequestIdNotFoundException(request.RequestId);

            return new LoanRequestDto
            {
                Id = request.RequestId,
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
            };
        }
    }
}
