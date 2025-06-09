using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Commands;

namespace MicroNet.Product.Application.Commands
{
    public record UpdateLoanCommand(Guid Id, LoanDto LoanDto, string UpdatedBy) : ICommand;
}
