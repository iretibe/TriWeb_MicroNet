using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Helpers;
using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Dtos.External;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Handlers.Commands
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public AddProductCommandHandler(IProductRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Product;

            var record = await _repository.GetByIdAsync(request.Product.Id);
            if (record is not null)
            {
                throw new ProductIdAlreadyExistsException(request.Product.Id);
            }

            var product = new Core.Entities.Product(
                request.Product.ProductCode,
                request.Product.ProductName,
                request.Product.Description,
                request.Product.Note,
                request.CreatedBy
            );
            await _repository.AddAsync(product);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.CreatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Created a Product",
                EntityType = nameof(Core.Entities.Product)
            });

            return product.Id;
        }
    }
}
