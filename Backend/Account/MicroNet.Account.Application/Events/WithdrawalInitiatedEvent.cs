using MicroNet.Account.Core.Events;

namespace MicroNet.Account.Application.Events
{
    public record WithdrawalInitiatedEvent(Guid AccountId) : IDomainEvent;
}
