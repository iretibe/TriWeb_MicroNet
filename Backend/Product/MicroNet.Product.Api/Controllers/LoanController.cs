using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Product.Api.Controllers
{
    public class LoanController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public LoanController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllLoans")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
        {
            var result = await _dispatcher.QueryAsync<GetAllLoansQuery, IEnumerable<LoanDto>>(new GetAllLoansQuery());

            return Ok(result);
        }

        [HttpGet("GetLoanById/{id}")]
        public async Task<ActionResult<LoanDto>> GetLoanById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetLoanByIdQuery, LoanDto>(new GetLoanByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] AddLoanCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateLoan/{id}")]
        public async Task<IActionResult> UpdateLoan(Guid id, [FromBody] UpdateLoanCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteLoan/{id}")]
        public async Task<IActionResult> DeleteLoan(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteLoanCommand(id));

            return NoContent();
        }
    }
}
