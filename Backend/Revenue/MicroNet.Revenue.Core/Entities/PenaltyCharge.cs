using MicroNet.Revenue.Core.ValueObjects;

namespace MicroNet.Revenue.Core.Entities
{
    public class PenaltyCharge : AggregateRoot
    {
        public string AccountNumber { get; private set; }
        public decimal Amount { get; private set; }
        public PenaltyReason Reason { get; private set; }
        public DateTime ChargedAt { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public PenaltyCharge() { } //For EF Core

        public PenaltyCharge(string accountNumber, decimal amount, PenaltyReason reason, string createdBy)
        {
            Id = Guid.NewGuid();
            AccountNumber = accountNumber;
            Amount = amount;
            Reason = reason;
            ChargedAt = DateTime.UtcNow;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }
    }
}
