using MediatR;

namespace MicroNet.Shared.CQRS.Commands
{
    //Registers all classes implementing ICommandHandler<T> from our assemblies.
    public static class CommandHandlerExtensions
    {
        // Sends a command that returns a result (e.g., DTO).
        public static async Task<TResponse> SendCommandAsync<TCommand, TResponse>(
            this IMediator mediator,
            TCommand command,
            CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>
        {
            return await mediator.Send(command, cancellationToken);
        }

        // Sends a command that returns nothing (Unit).
        public static async Task SendCommandAsync<TCommand>(
            this IMediator mediator,
            TCommand command,
            CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            await mediator.Send(command, cancellationToken);
        }
    }
}
