using MicroNet.Revenue.Core.ValueObjects;

namespace MicroNet.Revenue.Core.Entities
{
    public class ManagementFee : AggregateRoot
    {
        public string AccountNumber { get; private set; }
        public FeeStructure Fee { get; private set; }
        public decimal CalculatedAmount { get; private set; }
        public DateTime ChargedAt { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public ManagementFee() { } //For EF Core

        public ManagementFee(string accountNumber, FeeStructure fee, decimal calculatedAmount, string createdBy)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            Fee = fee;
            CalculatedAmount = calculatedAmount;
            ChargedAt = DateTime.UtcNow;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }
    }
}
