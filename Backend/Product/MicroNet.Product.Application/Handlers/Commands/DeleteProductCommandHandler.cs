using MediatR;
using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Helpers;
using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Dtos.External;
using MicroNet.Product.Core.Repositories;
using MicroNet.Product.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Handlers.Commands
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteProductCommandHandler(IProductRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null) throw new ProductIdNotFoundException(request.Id);

            var auditInfo = new AuditInfo(null, null, null, null, product!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(product);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.DeletedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(product, SerializationHelper.Settings),
                Method = "Deleted a Product",
                EntityType = nameof(Core.Entities.Product)
            });

            return Unit.Value;
        }
    }
}