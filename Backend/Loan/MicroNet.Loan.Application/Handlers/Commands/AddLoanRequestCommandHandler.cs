using MicroNet.Loan.Application.Commands;
using MicroNet.Loan.Core.Entities;
using MicroNet.Loan.Core.Repositories;
using MicroNet.Loan.Core.ValueObjects;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Handlers.Commands
{
    public class AddLoanRequestCommandHandler : ICommandHandler<AddLoanRequestCommand, Guid>
    {
        private readonly ILoanRequestRepository _repository;

        public AddLoanRequestCommandHandler(ILoanRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddLoanRequestCommand command, CancellationToken cancellationToken)
        {
            var dto = command.RequestDto;
            var documents = dto.SupportingDocuments.Select(d => new LoanDocument
            {
                FileName = d.FileName,
                FileUrl = d.FileUrl
            }).ToList();

            var loan = new LoanRequest(
                dto.ClientAccountNumber,
                dto.ClientAccountName,
                dto.BranchName,
                dto.LoanType,
                dto.InterestRate,
                dto.RepaymentPeriod,
                dto.RequestedAmount,
                dto.RequestedPrincipal,
                dto.RiskMargin,
                dto.InsuranceAmount,
                dto.DisbursementMedium,
                documents,
                dto.RequestedBy
            );

            await _repository.AddAsync(loan);
            return loan.Id;
        }
    }
}
