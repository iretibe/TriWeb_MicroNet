using MicroNet.Loan.Core.Dtos;
using MicroNet.Loan.Core.Entities;

namespace MicroNet.Loan.Application.Mappers
{
    public static class LoanRequestMapper
    {
        public static LoanRequestDto ToDto(this LoanRequest loan) => new LoanRequestDto
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
            RequestedBy = loan.AuditInfo.CreatedBy!,
            SupportingDocuments = loan.SupportingDocuments.Select(d => new LoanDocumentDto
            {
                FileName = d.FileName,
                FileUrl = d.FileUrl
            }).ToList(),
            Status = loan.Status.ToString()
        };
    }
}
