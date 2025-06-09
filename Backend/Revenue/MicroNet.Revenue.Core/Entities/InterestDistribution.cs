using MicroNet.Revenue.Core.ValueObjects;

namespace MicroNet.Revenue.Core.Entities
{
    public class InterestDistribution : AggregateRoot
    {
        public DistributionPeriod Period { get; private set; }
        public decimal TotalInterest { get; private set; }
        public List<AccountShare> DistributedTo { get; private set; } = new();
        public DateTime DistributedAt { get; private set; }
        public AuditInfo AuditInfo { get; private set; }

        public InterestDistribution() { } //For EF Core

        public InterestDistribution(DistributionPeriod period, decimal totalInterest, List<AccountShare> distributedTo, string createdBy)
        {
            Id = Guid.NewGuid();
            Period = period;
            TotalInterest = totalInterest;
            DistributedTo = distributedTo;
            DistributedAt = DateTime.UtcNow;
            AuditInfo = AuditInfo.CreateNew(createdBy);
        }
    }
}
