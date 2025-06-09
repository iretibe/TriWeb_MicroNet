using MicroNet.Shared.CQRS.Events;
using MicroNet.User.Application.Events.User;

namespace MicroNet.User.Application.Handlers.Events.User
{
    public class UserDeletedEventHandler : IEventHandler<UserDeletedEvent>
    {
        public Task HandleAsync(UserDeletedEvent @event, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
