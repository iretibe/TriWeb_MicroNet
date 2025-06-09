using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record UpdateProductCommand(Guid Id, ProductDto ProductDto, string UpdatedBy) : ICommand;
}
