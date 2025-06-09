using MediatR;
using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Helpers;
using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Dtos.External;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Handlers.Commands
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public UpdateProductCommandHandler(IProductRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);

            if (existing == null)
                throw new ProductIdNotFoundException(request.Id);

            var dto = request.ProductDto;

            existing.Update(
                dto.Id, dto.ProductName, dto.Description, dto.Note, request.UpdatedBy
            );

            await _repository.UpdateAsync(existing);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.UpdatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Updated a Product",
                EntityType = nameof(Core.Entities.Product)
            });

            return Unit.Value;
        }
    }
}