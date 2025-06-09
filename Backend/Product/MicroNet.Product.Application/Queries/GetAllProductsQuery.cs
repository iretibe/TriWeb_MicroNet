using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Queries
{
    public record GetAllProductsQuery() : IQuery<List<ProductDto>>;
}
