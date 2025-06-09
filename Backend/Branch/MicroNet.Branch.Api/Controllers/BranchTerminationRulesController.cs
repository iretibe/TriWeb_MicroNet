using MicroNet.Branch.Api.BranchTerminationRules.CreateBranchTerminationRule;
using MicroNet.Branch.Api.BranchTerminationRules.DeleteBranchTerminationRule;
using MicroNet.Branch.Api.BranchTerminationRules.GetAllBranchTerminationRules;
using MicroNet.Branch.Api.BranchTerminationRules.GetBranchTerminationRuleById;
using MicroNet.Branch.Api.BranchTerminationRules.UpdateBranchTerminationRule;
using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Branch.Api.Controllers
{
    public class BranchTerminationRulesController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public BranchTerminationRulesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllBranchTerminationRules")]
        public async Task<ActionResult<IEnumerable<BranchTerminationRuleDto>>> GetAllBranchTerminationRules()
        {
            var result = await _dispatcher.QueryAsync<GetAllBranchTerminationRulesQuery, IEnumerable<BranchTerminationRuleDto>>(new GetAllBranchTerminationRulesQuery());

            return Ok(result);
        }

        [HttpGet("GetBranchTerminationRuleById/{id}")]
        public async Task<ActionResult<BranchTerminationRuleDto>> GetBranchTerminationRuleById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetBranchTerminationRuleByIdQuery, BranchTerminationRuleDto>(new GetBranchTerminationRuleByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateBranchTerminationRule")]
        public async Task<IActionResult> CreateBranchTerminationRule([FromBody] CreateBranchTerminationRuleCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateBranchTerminationRule/{id}")]
        public async Task<IActionResult> UpdateBranchTerminationRule(Guid id, [FromBody] UpdateBranchTerminationRuleCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteBranchTerminationRule/{id}")]
        public async Task<IActionResult> DeleteBranchTerminationRule(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteBranchTerminationRuleCommand(id));

            return NoContent();
        }
    }
}
