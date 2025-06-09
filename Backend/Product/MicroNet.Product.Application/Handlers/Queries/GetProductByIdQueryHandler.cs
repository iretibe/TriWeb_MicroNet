using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Handlers.Queries
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var e = await _repository.GetByIdAsync(request.Id);
            if (e == null) throw new ProductIdNotFoundException(request.Id);

            return new ProductDto(e.Id, e.ProductCode, e.ProductName, e.Description, e.Note);
        }
    }
}
