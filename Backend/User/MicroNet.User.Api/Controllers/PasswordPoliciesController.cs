using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.Policy;
using MicroNet.User.Application.Queries.Policy;
using MicroNet.User.Core.Dto.Policy;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.User.Api.Controllers
{
    public class PasswordPoliciesController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public PasswordPoliciesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllPassPolicies")]
        public async Task<ActionResult<IEnumerable<PasswordPolicyDto>>> GetAllPassPolicies()
        {
            var result = await _dispatcher.QueryAsync<GetAllPasswordPoliciesQuery, IEnumerable<PasswordPolicyDto>>(new GetAllPasswordPoliciesQuery());

            return Ok(result);
        }

        [HttpGet("GetPassPolicyById/{id}")]
        public async Task<ActionResult<PasswordPolicyDto>> GetPassPolicyById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetPasswordPolicyByIdQuery, PasswordPolicyDto>(new GetPasswordPolicyByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreatePassPolicy")]
        public async Task<IActionResult> CreatePassPolicy([FromBody] AddPasswordPolicyCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdatePassPolicy/{id}")]
        public async Task<IActionResult> UpdatePassPolicy(Guid id, [FromBody] UpdatePasswordPolicyCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeletePassPolicy/{id}")]
        public async Task<IActionResult> DeletePassPolicy(Guid id)
        {
            await _dispatcher.SendAsync(new DeletePasswordPolicyCommand(id));

            return NoContent();
        }
    }
}
