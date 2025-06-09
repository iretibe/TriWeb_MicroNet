using MicroNet.Product.Core.ValueObjects;

namespace MicroNet.Product.Core.Entities
{
    public class Loan : AggregateRoot
    {
        public string LoanCode { get; private set; }
        public string LoanName { get; private set; }
        public decimal MaximumAmount { get; private set; }
        public decimal PercentageOfSavings { get; private set; }
        public decimal InterestRate { get; private set; }
        public int RepaymentPeriod { get; private set; }
        public int GracePeriod { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        private Loan() { }

        public Loan(string loanCode, string loanName, decimal maxAmount, decimal percentSavings,
                    decimal interestRate, int repaymentPeriod, int gracePeriod, string createdBy)
        {
            Id = Guid.NewGuid();
            LoanCode = loanCode;
            LoanName = loanName;
            MaximumAmount = maxAmount;
            PercentageOfSavings = percentSavings;
            InterestRate = interestRate;
            RepaymentPeriod = repaymentPeriod;
            GracePeriod = gracePeriod;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }

        public void Update(Guid id, string loanName, decimal maxAmount, 
            decimal percentSavings, decimal interestRate, int repaymentPeriod, int gracePeriod, string updatedBy)
        {
            Id = id;
            LoanName = loanName;
            MaximumAmount = maxAmount;
            PercentageOfSavings = percentSavings;
            InterestRate = interestRate;
            RepaymentPeriod = repaymentPeriod;
            GracePeriod = gracePeriod;
            AuditInfo.SetUpdated(updatedBy);
        }
    }
}
