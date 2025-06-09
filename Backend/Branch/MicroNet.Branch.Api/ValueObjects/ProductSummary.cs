namespace MicroNet.Branch.Api.ValueObjects
{
    public class ProductSummary
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public int NumberOfLoans { get; private set; }
        public decimal TotalLoanAmount { get; private set; }
        public decimal TotalInterest { get; private set; }
        public decimal TotalRepayment { get; private set; }
        public decimal ProcessingFees { get; private set; }
        public decimal PenaltyCharges { get; private set; }
        public decimal TotalLoanBalance { get; private set; }

        public decimal ProductAmount { get; private set; }
        public decimal Interest { get; private set; }
        public decimal Withdrawal { get; private set; }
        public decimal ManagementFees { get; private set; }
        public decimal Balance { get; private set; }

        private ProductSummary() { }

        public ProductSummary(int numberOfLoans, decimal totalLoanAmount, 
            decimal totalInterest, decimal totalRepayment, decimal processingFees, 
            decimal penaltyCharges, decimal totalLoanBalance, decimal productAmount, 
            decimal interest, decimal withdrawal, decimal managementFees, decimal balance)
        {
            NumberOfLoans = numberOfLoans;
            TotalLoanAmount = totalLoanAmount;
            TotalInterest = totalInterest;
            TotalRepayment = totalRepayment;
            ProcessingFees = processingFees;
            PenaltyCharges = penaltyCharges;
            TotalLoanBalance = totalLoanBalance;

            ProductAmount = productAmount;
            Interest = interest;
            Withdrawal = withdrawal;
            ManagementFees = managementFees;
            Balance = balance;
        }
    }
}
