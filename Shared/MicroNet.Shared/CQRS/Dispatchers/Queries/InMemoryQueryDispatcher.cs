using MediatR;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Shared.CQRS.Dispatchers.Queries
{
    public class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private readonly IMediator _mediator;

        public InMemoryQueryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
            where TResult : notnull
            => _mediator.Send(query, cancellationToken);
    }
}
