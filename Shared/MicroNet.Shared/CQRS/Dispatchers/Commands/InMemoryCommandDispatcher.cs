using MediatR;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Shared.CQRS.Dispatchers.Commands
{
    public class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;

        public InMemoryCommandDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
            => _mediator.Send(command, cancellationToken);

        public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
            => _mediator.Send(command, cancellationToken);
    }
}
