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
    public class DeleteLoanCommandHandler : ICommandHandler<DeleteLoanCommand>
    {
        private readonly ILoanRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public DeleteLoanCommandHandler(ILoanRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _repository.GetByIdAsync(request.Id);
            if (loan == null) throw new LoanIdNotFoundException(request.Id);

            var auditInfo = new AuditInfo(null, null, null, null, loan!.AuditInfo.DeletedBy, DateTime.Now);

            await _repository.DeleteAsync(loan);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = auditInfo.DeletedBy!,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(loan, SerializationHelper.Settings),
                Method = "Deleted a Loan",
                EntityType = nameof(Core.Entities.Loan)
            });

            return Unit.Value;
        }
    }
}