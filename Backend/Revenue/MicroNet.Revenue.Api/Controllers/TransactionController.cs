using MicroNet.Revenue.Application.Commands;
using MicroNet.Revenue.Application.Queries;
using MicroNet.Revenue.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Revenue.Api.Controllers
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

    [HttpPost("CreateTransaction")]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        //command.CreatedBy = UsrId!.ToString();
        var result = await _dispatcher.SendAsync(command);

        return Ok(result);
    }

    [HttpPut("UpdateTransaction/{id}")]
    public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID in URL does not match ID in payload.");

        await _dispatcher.SendAsync(command);

        return NoContent();
    }

    [HttpDelete("DeleteTransaction/{id}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        await _dispatcher.SendAsync(new DeleteTransactionCommand(id));

        return NoContent();
    }
}
}
