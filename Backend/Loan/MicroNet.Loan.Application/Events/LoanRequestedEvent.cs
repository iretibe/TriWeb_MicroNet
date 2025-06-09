using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Loan.Application.Events
{
    public record LoanRequestedEvent(Guid RequestId, string triggeredBy) : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.UtcNow;
        public string TriggeredBy => triggeredBy;
    }
}
