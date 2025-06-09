using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Revenue.Api.Controllers
{
    public class PenaltyChargeController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public PenaltyChargeController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllPenaltyCharges")]
        public async Task<ActionResult<IEnumerable<PenaltyChargeDto>>> GetAllPenaltyCharges()
        {
            var result = await _dispatcher.QueryAsync<GetAllPenaltyChargeQuery, IEnumerable<PenaltyChargeDto>>(new GetAllPenaltyChargeQuery());

            return Ok(result);
        }

        [HttpGet("GetPenaltyChargeById/{id}")]
        public async Task<ActionResult<PenaltyChargeDto>> GetPenaltyChargeById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetPenaltyChargeByIdQuery, PenaltyChargeDto>(new GetPenaltyChargeByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreatePenaltyCharge")]
        public async Task<IActionResult> CreatePenaltyCharge([FromBody] CreatePenaltyChargeCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdatePenaltyCharge/{id}")]
        public async Task<IActionResult> UpdatePenaltyCharge(Guid id, [FromBody] UpdatePenaltyChargeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeletePenaltyCharge/{id}")]
        public async Task<IActionResult> DeletePenaltyCharge(Guid id)
        {
            await _dispatcher.SendAsync(new DeletePenaltyChargeCommand(id));

            return NoContent();
        }
    }
}
