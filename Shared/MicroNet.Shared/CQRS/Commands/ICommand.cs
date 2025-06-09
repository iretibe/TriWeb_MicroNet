using MediatR;

namespace MicroNet.Shared.CQRS.Commands
{
    public interface ICommand : ICommand<Unit>
    {

    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
