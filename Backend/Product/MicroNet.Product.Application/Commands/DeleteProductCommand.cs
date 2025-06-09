using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record DeleteProductCommand(Guid Id) : ICommand;
}
