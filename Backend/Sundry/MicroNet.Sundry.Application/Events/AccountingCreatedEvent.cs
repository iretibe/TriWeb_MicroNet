using MicroNet.Shared.CQRS.Events;

namespace MicroNet.Sundry.Application.Events
{
    public class AccountingCreatedEvent : EventBase
    {
        public Guid AccountId { get; init; }
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;

        public AccountingCreatedEvent() { } //For deserialization

        public AccountingCreatedEvent(Guid accountId, string code, string name, string triggeredBy)
        {
            AccountId = accountId;
            Code = code;
            Name = name;
            TriggeredBy = triggeredBy;
        }
    }
}
