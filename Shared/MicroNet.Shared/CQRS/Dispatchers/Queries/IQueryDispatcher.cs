using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Shared.CQRS.Dispatchers.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default) where TResult : notnull;
    }
}
