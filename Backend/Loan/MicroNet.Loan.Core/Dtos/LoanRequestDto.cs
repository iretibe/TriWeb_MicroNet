namespace MicroNet.Loan.Core.Dtos
{
    public class LoanRequestDto
    {
        public Guid Id { get; set; }
        public string ClientAccountNumber { get; set; } = default!;
        public string ClientAccountName { get; set; } = default!;
        public string BranchName { get; set; } = default!;
        public string LoanType { get; set; } = default!;
        public decimal InterestRate { get; set; }
        public int RepaymentPeriod { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal RequestedPrincipal { get; set; }
        public decimal RiskMargin { get; set; }
        public decimal InsuranceAmount { get; set; }
        public string DisbursementMedium { get; set; } = default!;
        public string Status { get; set; } = "Pending";
        public string RequestedBy { get; set; } = default!;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public List<LoanDocumentDto> SupportingDocuments { get; set; } = new();
    }
}
