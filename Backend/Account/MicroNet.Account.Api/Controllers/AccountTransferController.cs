using MicroNet.Account.Application.Commands;
using MicroNet.Account.Application.Queries;
using MicroNet.Account.Core.Entities;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Account.Api.Controllers
{
    public class AccountTransferController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AccountTransferController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllAccountTransfers")]
        public async Task<ActionResult<IEnumerable<AccountTransfer>>> GetAllAccountTransfers()
        {
            var result = await _dispatcher.QueryAsync<GetAllAccountTransferQuery, IEnumerable<AccountTransfer>>(new GetAllAccountTransferQuery());

            return Ok(result);
        }

        [HttpGet("GetAccountTransferById/{id}")]
        public async Task<ActionResult<AccountTransfer>> GetAccountTransferById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetAccountTransferByIdQuery, AccountTransfer>(new GetAccountTransferByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateAccountTransfer")]
        public async Task<IActionResult> CreateAccountTransfer([FromBody] TransferAccountCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
