using MicroNet.Loan.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Loan.Application.Commands
{
    public record SubmitLoanRequestCommand(LoanRequestDto Request) : ICommand<Guid>;
}
