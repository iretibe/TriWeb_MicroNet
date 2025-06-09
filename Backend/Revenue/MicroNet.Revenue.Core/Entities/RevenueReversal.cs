using MicroNet.Revenue.Core.ValueObjects;

namespace MicroNet.Revenue.Core.Entities
{
    public class RevenueReversal : AggregateRoot
    {
        public Guid OriginalTransactionId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime ReversedAt { get; private set; }
        public ReversalReason Reason { get; private set; }
        public string ReversedBy { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public RevenueReversal() { } //For EF Core

        public RevenueReversal(Guid originalTransactionId, decimal amount, 
            ReversalReason reason, string reversedBy, string createdBy)
        {
            Id = Guid.NewGuid();
            OriginalTransactionId = originalTransactionId;
            Amount = amount;
            Reason = reason;
            ReversedAt = DateTime.UtcNow;
            ReversedBy = reversedBy;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }
    }
}
