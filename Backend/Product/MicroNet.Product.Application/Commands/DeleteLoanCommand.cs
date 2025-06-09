using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record DeleteLoanCommand(Guid Id) : ICommand;
}
