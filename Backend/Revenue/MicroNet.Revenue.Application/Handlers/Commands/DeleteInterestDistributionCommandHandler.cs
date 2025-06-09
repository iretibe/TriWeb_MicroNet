using MediatR;
using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    public class DeleteInterestDistributionCommandHandler : ICommandHandler<DeleteInterestDistributionCommand>
    {
        private readonly IInterestDistributionRepository _repository;
        //private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteInterestDistributionCommandHandler(IInterestDistributionRepository repository)
        {
            _repository = repository;
            //_auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteInterestDistributionCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result == null) throw new InterestDistributionIdNotFoundException(request.Id);

            //var auditInfo = new AuditInfo(null, null, null, null, loan!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(result.Id);

            //await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            //{
            //    Id = Guid.NewGuid(),
            //    AuditDate = DateTime.UtcNow,
            //    UserId = auditInfo.DeletedBy!,
            //    Data = Newtonsoft.Json.JsonConvert.SerializeObject(loan, SerializationHelper.Settings),
            //    Method = "Deleted a Loan",
            //    EntityType = nameof(Core.Entities.Loan)
            //});

            return Unit.Value;
        }
    }
}
