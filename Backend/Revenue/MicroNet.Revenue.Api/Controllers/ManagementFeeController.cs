using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Revenue.Api.Controllers
{
    public class ManagementFeeController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public ManagementFeeController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllManagementFees")]
        public async Task<ActionResult<IEnumerable<ManagementFeeDto>>> GetAllManagementFees()
        {
            var result = await _dispatcher.QueryAsync<GetAllManagementFeeQuery, IEnumerable<ManagementFeeDto>>(new GetAllManagementFeeQuery());

            return Ok(result);
        }

        [HttpGet("GetManagementFeeById/{id}")]
        public async Task<ActionResult<ManagementFeeDto>> GetManagementFeeById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetManagementFeeByQuery, ManagementFeeDto>(new GetManagementFeeByQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateManagementFee")]
        public async Task<IActionResult> CreateManagementFee([FromBody] CreateManagementFeeCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateManagementFee/{id}")]
        public async Task<IActionResult> UpdateManagementFee(Guid id, [FromBody] UpdateManagementFeeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteManagementFee/{id}")]
        public async Task<IActionResult> DeleteManagementFee(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteManagementFeeCommand(id));

            return NoContent();
        }
    }
}
