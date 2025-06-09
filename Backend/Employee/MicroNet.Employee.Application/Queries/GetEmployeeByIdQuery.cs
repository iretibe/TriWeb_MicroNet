using MicroNet.Employee.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Employee.Application.Queries
{
    public record GetEmployeeByIdQuery(Guid Id) : IQuery<EmployeeDto>;
}
