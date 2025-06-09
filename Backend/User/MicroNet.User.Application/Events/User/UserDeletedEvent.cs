using MicroNet.Shared.CQRS.Events;

namespace MicroNet.User.Application.Events.User
{
    public class UserDeletedEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public string TriggeredBy { get; private set; }

        public string UserId { get; private set; }
        public string DeletedBy { get; private set; }

        public UserDeletedEvent(string triggeredBy, string userId, string deletedBy)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            TriggeredBy = triggeredBy;

            UserId = userId;
            DeletedBy = deletedBy;
        }
    }
}
