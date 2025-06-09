using MediatR;
using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Exceptions;
using MicroNet.Revenue.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Revenue.Application.Handlers.Commands
{
    public class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionRepository _repository;

        public DeleteTransactionCommandHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);
            if (result == null) throw new TransactionIdNotFoundException(request.Id);

            //var auditInfo = new AuditInfo(null, null, null, null, loan!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(request.Id);

            //await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            //{
            //    Id = Guid.NewGuid(),
            //    AuditDate = DateTime.UtcNow,
            //    UserId = auditInfo.DeletedBy!,
            //    Data = Newtonsoft.Json.JsonConvert.SerializeObject(loan, SerializationHelper.Settings),
            //    Method = "Deleted a Transaction",
            //    EntityType = nameof(Core.Entities.Transaction)
            //});

            return Unit.Value;
        }
    }
}
