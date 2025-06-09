using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Product.Application.Queries
{
    public record GetAllLoansQuery() : IQuery<List<LoanDto>>;
}
