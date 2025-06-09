using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Shared.CQRS.Dispatchers
{
    public class InMemoryDispatcher : IDispatcher
    {
        private readonly IMediator _mediator;

        public InMemoryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
            => _mediator.Send(command, cancellationToken);

        public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
            => _mediator.Send(command, cancellationToken);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            where TResult : notnull
            => _mediator.Send(query, cancellationToken);
    }
}
