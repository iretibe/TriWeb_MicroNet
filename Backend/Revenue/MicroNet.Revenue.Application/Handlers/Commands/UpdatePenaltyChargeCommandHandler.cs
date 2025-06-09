using MediatR;
using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    public class UpdatePenaltyChargeCommandHandler : ICommandHandler<UpdatePenaltyChargeCommand>
    {
        private readonly IPenaltyChargeRepository _repository;
        //private readonly IAuditLogServiceClient _auditLogClient;

        public UpdatePenaltyChargeCommandHandler(IPenaltyChargeRepository repository) //, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            //_auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdatePenaltyChargeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);

            if (existing == null)
                throw new PenaltyChargeIdNotFoundException(request.Id);

            var dto = request.ChargeDto;

            //existing.Update(
            //    dto.Id, dto.ProductName, dto.Description, dto.Note, request.UpdatedBy
            //);

            await _repository.UpdateAsync(existing);

            //await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            //{
            //    Id = Guid.NewGuid(),
            //    AuditDate = DateTime.UtcNow,
            //    UserId = request.UpdatedBy,
            //    Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
            //    Method = "Updated a Product",
            //    EntityType = nameof(Core.Entities.Product)
            //});

            return Unit.Value;
        }
    }
}
