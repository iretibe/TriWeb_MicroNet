using MicroNet.Employee.Core.Dtos;
using MicroNet.Shared.CQRS.Queries;

namespace MicroNet.Employee.Application.Queries
{
    public record GetAllEmployeesQuery : IQuery<List<EmployeeDto>>;
}
