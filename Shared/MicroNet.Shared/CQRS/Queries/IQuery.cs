using MediatR;

namespace MicroNet.Shared.CQRS.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {
    }
}
