using MicroNet.Branch.Api.Branches.CreateBranch;
using MicroNet.Branch.Api.Branches.DeleteBranch;
using MicroNet.Branch.Api.Branches.GetAllBranches;
using MicroNet.Branch.Api.Branches.GetBranchById;
using MicroNet.Branch.Api.Branches.UpdateBranch;
using MicroNet.Branch.Api.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Branch.Api.Controllers
{
    public class BranchesController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public BranchesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllBranches")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAllBranches()
        {
            var result = await _dispatcher.QueryAsync<GetAllBranchesQuery, IEnumerable<BranchDto>>(new GetAllBranchesQuery());

            return Ok(result);
        }

        [HttpGet("GetBranchById/{id}")]
        public async Task<ActionResult<BranchDto>> GetBranchById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetBranchByIdQuery, BranchDto>(new GetBranchByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateBranch")]
        public async Task<IActionResult> CreateBranch([FromBody] CreateBranchCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateBranch/{id}")]
        public async Task<IActionResult> UpdateBranch(Guid id, [FromBody] UpdateBranchCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteBranch/{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteBranchCommand(id));

            return NoContent();
        }
    }
}
