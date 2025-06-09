using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Handlers.Queries
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.ProductCode, p.ProductName, p.Description, p.Note)).ToList();
        }
    }
}
