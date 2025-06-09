using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Queries
{
    public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;
}
