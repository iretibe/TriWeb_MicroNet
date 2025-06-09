namespace MicroNet.Branch.Api.Dtos
{
    public class LoanSummaryDto
    {
        public int NumberOfLoans { get; set; }
        public decimal TotalLoanAmount { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepayment { get; set; }
        public decimal ProcessingFees { get; set; }
        public decimal PenaltyCharges { get; set; }
        public decimal TotalLoanBalance { get; set; }
    }
}
