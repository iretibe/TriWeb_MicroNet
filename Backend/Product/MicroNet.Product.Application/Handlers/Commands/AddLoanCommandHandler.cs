using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Exceptions;
using MicroNet.Product.Application.Helpers;
using MicroNet.Product.Core.Clients;
using MicroNet.Product.Core.Dtos.External;
using MicroNet.Product.Core.Entities;
using MicroNet.Product.Core.Repositories;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Handlers.Commands
{
    public class AddLoanCommandHandler : ICommandHandler<AddLoanCommand, Guid>
    {
        private readonly ILoanRepository _repository;
        private readonly IAuditLogServiceClient _auditLogClient;

        public AddLoanCommandHandler(ILoanRepository repository, IAuditLogServiceClient auditLogClient)
        {
            _repository = repository;
            _auditLogClient = auditLogClient;
        }

        public async Task<Guid> Handle(AddLoanCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Loan;

            var record = await _repository.GetByIdAsync(request.Loan.Id);
            if (record is not null)
            {
                throw new LoanIdAlreadyExistsException(request.Loan.Id);
            }

            var loan = new Loan(
                request.Loan.LoanCode,
                request.Loan.LoanName,
                request.Loan.MaximumAmount,
                request.Loan.PercentageOfSavings,
                request.Loan.InterestRate,
                request.Loan.RepaymentPeriod,
                request.Loan.GracePeriod,
                request.CreatedBy
            );
            await _repository.AddAsync(loan);

            await _auditLogClient.CreateAuditLogAsync(new AuditLogDto
            {
                Id = Guid.NewGuid(),
                AuditDate = DateTime.UtcNow,
                UserId = request.CreatedBy,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(dto, SerializationHelper.Settings),
                Method = "Created a Loan",
                EntityType = nameof(Core.Entities.Loan)
            });

            return loan.Id;
        }
    }
}
