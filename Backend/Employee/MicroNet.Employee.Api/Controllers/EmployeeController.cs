using MicroNet.Employee.Application.Commands;
using MicroNet.Employee.Application.Queries;
using MicroNet.Employee.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Employee.Api.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public EmployeeController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            var result = await _dispatcher.QueryAsync<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>(new GetAllEmployeesQuery());

            return Ok(result);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetEmployeeByIdQuery, EmployeeDto>(new GetEmployeeByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] AddEmployeeCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteEmployeeCommand(id));

            return NoContent();
        }
    }
}
