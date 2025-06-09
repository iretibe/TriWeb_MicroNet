using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Events.User;

namespace MicroNet.User.Application.Handlers.Events.User
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        public Task HandleAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
