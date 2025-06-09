using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Revenue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueReversalController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public RevenueReversalController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllRevenueReversals")]
        public async Task<ActionResult<IEnumerable<RevenueReversalDto>>> GetAllRevenueReversals()
        {
            var result = await _dispatcher.QueryAsync<GetAllRevenueReversalQuery, IEnumerable<RevenueReversalDto>>(new GetAllRevenueReversalQuery());

            return Ok(result);
        }

        [HttpGet("GetRevenueReversalById/{id}")]
        public async Task<ActionResult<RevenueReversalDto>> GetRevenueReversalById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetRevenueReversalByIdQuery, RevenueReversalDto>(new GetRevenueReversalByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateRevenueReversal")]
        public async Task<IActionResult> CreateRevenueReversal([FromBody] CreateRevenueReversalCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateRevenueReversal/{id}")]
        public async Task<IActionResult> UpdateRevenueReversal(Guid id, [FromBody] UpdateRevenueReversalCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteRevenueReversal/{id}")]
        public async Task<IActionResult> DeleteRevenueReversal(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteRevenueReversalCommand(id));

            return NoContent();
        }
    }
}
