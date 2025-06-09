using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Events.User;

namespace MicroNet.User.Application.Handlers.Events.User
{
    public class UserUpdatedEventHandler : IEventHandler<UserUpdatedEvent>
    {
        public Task HandleAsync(UserUpdatedEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
