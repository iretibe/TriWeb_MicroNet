using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Account.Api.Controllers
{
    public class AccountWithdrawalController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AccountWithdrawalController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllWithdrawals")]
        public async Task<ActionResult<IEnumerable<Withdrawal>>> GetAllWithdrawals()
        {
            var result = await _dispatcher.QueryAsync<GetAllWithdrawalQuery, IEnumerable<Withdrawal>>(new GetAllWithdrawalQuery());

            return Ok(result);
        }

        [HttpGet("GetWithdrawalById/{id}")]
        public async Task<ActionResult<Withdrawal>> GetWithdrawalById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetWithdrawalByIdQuery, Withdrawal>(new GetWithdrawalByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateWithdrawal")]
        public async Task<IActionResult> CreateWithdrawal([FromBody] InitiateWithdrawalCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
