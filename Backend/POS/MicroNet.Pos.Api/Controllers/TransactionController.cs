using MicroNet.Pos.Application.Commands;
using MicroNet.Pos.Application.Queries;
using MicroNet.Pos.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Pos.Api.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public TransactionController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllTransactions()
        {
            var result = await _dispatcher.QueryAsync<GetAllTransactionsQuery, IEnumerable<TransactionDto>>(new GetAllTransactionsQuery());

            return Ok(result);
        }

        [HttpGet("GetTransactionById/{id}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetTransactionByIdQuery, TransactionDto>(new GetTransactionByIdQuery(id));

            return Ok(result);
        }

        [HttpGet("GetTransactionByAccountNumber/{accountNumber}")]
        public async Task<ActionResult<TransactionDto>> GetTransactionByAccountNumber(string accountNumber)
        {
            var result = await _dispatcher.QueryAsync<GetTransactionByAccountNumberQuery, TransactionDto>(new GetTransactionByAccountNumberQuery(accountNumber));

            return Ok(result);
        }

        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] SubmitTransactionCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
