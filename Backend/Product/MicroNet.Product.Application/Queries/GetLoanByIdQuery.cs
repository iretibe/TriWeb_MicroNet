using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Queries
{
    public record GetLoanByIdQuery(Guid Id) : IQuery<LoanDto>;
}
