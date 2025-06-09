using MicroNet.Client.Application.Commands;
using MicroNet.Client.Application.Queries;
using MicroNet.Client.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Client.Api.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public ClientController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllClients")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAllClients()
        {
            var result = await _dispatcher.QueryAsync<GetAllClientsQuery, IEnumerable<ClientDto>>(new GetAllClientsQuery());

            return Ok(result);
        }

        [HttpGet("GetClientById/{id}")]
        public async Task<ActionResult<ClientDto>> GetClientById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetClientByIdQuery, ClientDto>(new GetClientByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] AddClientCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateClient/{id}")]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteClient/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteClientCommand(id));

            return NoContent();
        }
    }
}
