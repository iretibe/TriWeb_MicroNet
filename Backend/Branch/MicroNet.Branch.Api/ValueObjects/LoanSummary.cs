namespace MicroNet.Branch.Api.ValueObjects
{
    public class LoanSummary
    {
        public int NumberOfLoans { get; private set; }
        public decimal TotalLoanAmount { get; private set; }
        public decimal TotalInterest { get; private set; }
        public decimal TotalRepayment { get; private set; }
        public decimal ProcessingFees { get; private set; }
        public decimal PenaltyCharges { get; private set; }
        public decimal TotalLoanBalance { get; private set; }

        private LoanSummary() { }

        public LoanSummary(int numberOfLoans, decimal totalLoanAmount, decimal totalInterest,
                           decimal totalRepayment, decimal processingFees, decimal penaltyCharges, decimal totalLoanBalance)
        {
            NumberOfLoans = numberOfLoans;
            TotalLoanAmount = totalLoanAmount;
            TotalInterest = totalInterest;
            TotalRepayment = totalRepayment;
            ProcessingFees = processingFees;
            PenaltyCharges = penaltyCharges;
            TotalLoanBalance = totalLoanBalance;
        }
    }
}
