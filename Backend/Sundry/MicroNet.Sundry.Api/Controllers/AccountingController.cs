using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.Sundry.Application.Commands;
using MicroNet.Sundry.Application.Queries;
using MicroNet.Sundry.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Sundry.Api.Controllers
{
    public class AccountingController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AccountingController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllAccountings")]
        public async Task<ActionResult<IEnumerable<AccountingDto>>> GetAllAccountings()
        {
            var result = await _dispatcher.QueryAsync<GetAllAccountDefinitionsQuery, IEnumerable<AccountingDto>>(new GetAllAccountDefinitionsQuery());

            return Ok(result);
        }

        [HttpGet("GetAccountingById/{id}")]
        public async Task<ActionResult<AccountingDto>> GetAccountingById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetAccountDefinitionsByIdQuery, AccountingDto>(new GetAccountDefinitionsByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateAccounting")]
        public async Task<IActionResult> CreateAccounting([FromBody] CreateAccountDefinitionCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
