using MediatR;

namespace MicroNet.Shared.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
       where TQuery : IQuery<TResponse>
       where TResponse : notnull
    {
    }
}
