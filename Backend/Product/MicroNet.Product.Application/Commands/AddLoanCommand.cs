using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record AddLoanCommand(LoanDto Loan, string CreatedBy) : ICommand<Guid>;
}
