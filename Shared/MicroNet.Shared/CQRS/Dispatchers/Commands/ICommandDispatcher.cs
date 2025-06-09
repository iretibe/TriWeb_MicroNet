using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Shared.CQRS.Dispatchers.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
    }
}
