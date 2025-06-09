using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Loan.Application.Events
{
    public record LoanApprovedEvent(Guid requestId, string approvedBy, string triggeredBy) : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.UtcNow;
        public string TriggeredBy => triggeredBy;
    }
}
