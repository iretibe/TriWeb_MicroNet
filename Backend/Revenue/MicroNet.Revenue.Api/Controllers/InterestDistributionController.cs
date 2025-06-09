using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Revenue.Api.Controllers
{
    public class InterestDistributionController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public InterestDistributionController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllInterestDistributions")]
        public async Task<ActionResult<IEnumerable<InterestDistributionDto>>> GetAllInterestDistributions()
        {
            var result = await _dispatcher.QueryAsync<GetAllInterestDistributionQuery, IEnumerable<InterestDistributionDto>>(new GetAllInterestDistributionQuery());

            return Ok(result);
        }

        [HttpGet("GetInterestDistributionById/{id}")]
        public async Task<ActionResult<InterestDistributionDto>> GetInterestDistributionById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetInterestDistributionByIdQuery, InterestDistributionDto>(new GetInterestDistributionByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateInterestDistribution")]
        public async Task<IActionResult> CreateInterestDistribution([FromBody] CreateInterestDistributionCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateInterestDistribution/{id}")]
        public async Task<IActionResult> UpdateInterestDistribution(Guid id, [FromBody] UpdateInterestDistributionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteInterestDistribution/{id}")]
        public async Task<IActionResult> DeleteInterestDistribution(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteInterestDistributionCommand(id));

            return NoContent();
        }
    }
}
