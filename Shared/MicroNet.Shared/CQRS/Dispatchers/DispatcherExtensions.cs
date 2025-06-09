using MediatR;
using MicroNet.Shared.CQRS.Commands;
using MicroNet.Shared.CQRS.Dispatchers.Commands;
using MicroNet.Shared.CQRS.Dispatchers.Queries;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Shared.CQRS.Dispatchers
{
    public static class DispatcherExtensions
    {
        public static Task SendAsync<TCommand>(this ICommandDispatcher dispatcher, TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<Unit>
            => dispatcher.SendAsync(command, cancellationToken);

        public static Task<TResult> SendAsync<TCommand, TResult>(this ICommandDispatcher dispatcher, TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResult>
            => dispatcher.SendAsync(command, cancellationToken);

        public static Task<TResult> QueryAsync<TQuery, TResult>(this IQueryDispatcher dispatcher, TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResult>
            where TResult : notnull
            => dispatcher.QueryAsync(query, cancellationToken);
    }
}
