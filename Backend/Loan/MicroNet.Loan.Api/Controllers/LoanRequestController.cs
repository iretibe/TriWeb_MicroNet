using MicroNet.Loan.Application.Commands;
using MicroNet.Loan.Application.Queries;
using MicroNet.Loan.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Loan.Api.Controllers
{
    public class LoanRequestController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public LoanRequestController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllLoanRequests")]
        public async Task<ActionResult<IEnumerable<LoanRequestDto>>> GetAllLoanRequests()
        {
            var result = await _dispatcher.QueryAsync<GetAllLoanRequestsQuery, IEnumerable<LoanRequestDto>>(new GetAllLoanRequestsQuery());

            return Ok(result);
        }

        [HttpGet("GetLoanRequestById/{id}")]
        public async Task<ActionResult<LoanRequestDto>> GetLoanRequestById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetLoanRequestByIdQuery, LoanRequestDto>(new GetLoanRequestByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateLoanRequest")]
        public async Task<IActionResult> CreateLoanRequest([FromBody] AddLoanRequestCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("SubmitLoanRequest/{id}")]
        public async Task<IActionResult> SubmitLoanRequest(Guid id, [FromBody] SubmitLoanRequestCommand command)
        {
            if (id != command.Request.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPut("ApproveLoanRequest/{id}")]
        public async Task<IActionResult> ApproveLoanRequest(Guid id, [FromBody] ApproveLoanCommand command)
        {
            if (id != command.RequestId)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPut("CancelLoanRequest/{id}")]
        public async Task<IActionResult> CancelLoanRequest(Guid id, [FromBody] CancelLoanCommand command)
        {
            if (id != command.RequestId)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPut("RejectLoanRequest/{id}")]
        public async Task<IActionResult> RejectLoanRequest(Guid id, [FromBody] RejectLoanCommand command)
        {
            if (id != command.RequestId)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpPut("SuspendLoanRequest/{id}")]
        public async Task<IActionResult> SuspendLoanRequest(Guid id, [FromBody] SuspendLoanCommand command)
        {
            if (id != command.RequestId)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}
