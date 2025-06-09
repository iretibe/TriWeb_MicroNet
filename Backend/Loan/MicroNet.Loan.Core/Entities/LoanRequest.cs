using MicroNet.Loan.Core.Enums;
using MicroNet.Loan.Core.ValueObjects;

namespace MicroNet.Loan.Core.Entities
{
    public class LoanRequest : AggregateRoot
    {
        public string AccountNumber { get; private set; } = default!;
        public string ClientName { get; private set; } = default!;
        public string Branch { get; private set; } = default!;
        public string LoanType { get; private set; } = default!;
        public decimal InterestRate { get; private set; }
        public int RepaymentPeriod { get; private set; }
        public decimal MaximumAmount { get; private set; }
        public decimal RequestedPrincipal { get; private set; }
        public decimal RiskMargin { get; private set; }
        public decimal InsuranceAmount { get; private set; }
        public string DisbursementMedium { get; private set; } = default!;
        public List<LoanDocument> SupportingDocuments { get; private set; } = new();
        public LoanStatus Status { get; private set; }
        public string? ReviewerComment { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private LoanRequest() { }

        public LoanRequest(string accountNumber, string clientName, string branch, string loanType,
            decimal interestRate, int repaymentPeriod, decimal maxAmount, decimal requestedPrincipal,
            decimal riskMargin, decimal insuranceAmount, string disbursementMedium, List<LoanDocument> documents,
            string createdBy)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            ClientName = clientName;
            Branch = branch;
            LoanType = loanType;
            InterestRate = interestRate;
            RepaymentPeriod = repaymentPeriod;
            MaximumAmount = maxAmount;
            RequestedPrincipal = requestedPrincipal;
            RiskMargin = riskMargin;
            InsuranceAmount = insuranceAmount;
            DisbursementMedium = disbursementMedium;
            SupportingDocuments = documents;
            Status = LoanStatus.Pending;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Approve(string approvedBy)
        {
            Status = LoanStatus.Approved;
            AuditInfo.SetUpdated(approvedBy);
        }

        public void Suspend(string suspendedBy, string reason)
        {
            Status = LoanStatus.Suspended;
            ReviewerComment = reason;
            AuditInfo.SetUpdated(suspendedBy);
        }

        public void Reject(string rejectedBy, string reason)
        {
            Status = LoanStatus.Rejected;
            ReviewerComment = reason;
            AuditInfo.SetUpdated(rejectedBy);
        }

        public void Cancel(string cancelledBy, string reason)
        {
            Status = LoanStatus.Cancelled;
            ReviewerComment = reason;
            AuditInfo.SetUpdated(cancelledBy);
        }
    }
}
