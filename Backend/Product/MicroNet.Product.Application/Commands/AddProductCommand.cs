using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record AddProductCommand(ProductDto Product, string CreatedBy) : ICommand<Guid>;
}
