using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Account.Api.Controllers
{
    public class AccountTerminationController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AccountTerminationController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllAccountTerminations")]
        public async Task<ActionResult<IEnumerable<AccountTermination>>> GetAllAccountTerminations()
        {
            var result = await _dispatcher.QueryAsync<GetAllAccountTerminationQuery, IEnumerable<AccountTermination>>(new GetAllAccountTerminationQuery());

            return Ok(result);
        }

        [HttpGet("GetAccountTerminationById/{id}")]
        public async Task<ActionResult<AccountTermination>> GetAccountTerminationById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetAccountTerminationByIdQuery, AccountTermination>(new GetAccountTerminationByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateAccountTermination")]
        public async Task<IActionResult> CreateAccountTermination([FromBody] TerminateAccountCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
