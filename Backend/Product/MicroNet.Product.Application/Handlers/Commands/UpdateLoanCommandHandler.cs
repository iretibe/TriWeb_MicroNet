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
    internal class UpdateLoanCommandHandler : ICommandHandler<UpdateLoanCommand>
    {
        private readonly ILoanRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public UpdateLoanCommandHandler(ILoanRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Unit> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);

            if (existing == null)
                throw new LoanIdNotFoundException(request.Id);

            var dto = request.LoanDto;

            existing.Update(
                dto.Id, dto.LoanName, dto.MaximumAmount, dto.PercentageOfSavings, dto.InterestRate,
                dto.RepaymentPeriod, dto.GracePeriod, request.UpdatedBy
            );

            await _repository.UpdateAsync(existing);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.UpdatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Updated a Loan",
                EntityType = nameof(Core.Entities.Loan)
            });

            return Unit.Value;
        }
    }
}