using MediatR;

namespace MicroNet.Shared.CQRS.Queries
{
    public static class QueryHandlerExtensions
    {
        // Sends a query and returns a response.
        public static async Task<TResponse> SendQueryAsync<TQuery, TResponse>(
           this IMediator mediator, TQuery query, CancellationToken cancellationToken = default)
           where TQuery : IQuery<TResponse>
           where TResponse : notnull
        {
            return await mediator.Send(query, cancellationToken);
        }
    }
}
