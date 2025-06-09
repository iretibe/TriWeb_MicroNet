using MicroNet.Loan.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Loan.Application.Queries
{
    public record GetLoanRequestByIdQuery(Guid RequestId) : IQuery<LoanRequestDto>;
}
