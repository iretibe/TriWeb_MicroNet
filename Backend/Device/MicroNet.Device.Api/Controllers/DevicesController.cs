using MicroNet.Device.Application.Commands;
using MicroNet.Device.Application.Dto;
using MicroNet.Device.Application.Queries;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroNet.Device.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public DevicesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllDevices")]
        public async Task<ActionResult<IEnumerable<AllDeviceDto>>> GetAllDevices()
        {
            var result = await _dispatcher.QueryAsync<GetDevicesQuery, IEnumerable<AllDeviceDto>>(new GetDevicesQuery());

            return Ok(result);
        }

        [HttpGet("GetDeviceById/{id}")]
        public async Task<ActionResult<AllDeviceDto>> GetDeviceById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetDeviceByIdQuery, AllDeviceDto>(new GetDeviceByIdQuery(id));
            
            return Ok(result);
        }

        [HttpGet("GetDeviceByName/{name}")]
        public async Task<ActionResult<AllDeviceDto>> GetDeviceByName(string name)
        {
            var result = await _dispatcher.QueryAsync<GetDeviceByNameQuery, AllDeviceDto>(new GetDeviceByNameQuery(name));

            return Ok(result);
        }

        [HttpGet("GetDeviceNameById/{id}")]
        public async Task<ActionResult<AllDeviceNameDto>> GetDeviceNameById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetDeviceNameByIdQuery, AllDeviceNameDto>(new GetDeviceNameByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateDevice")]
        public async Task<IActionResult> CreateDevice([FromBody] AddDeviceCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);
            
            //return CreatedAtAction(nameof(GetDeviceById), new { id = command.Id }, null);
            return Ok(result);
        }

        [HttpPut("UpdateDevice/{id}")]
        public async Task<IActionResult> UpdateDevice(Guid id, [FromBody] UpdateDeviceCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteDevice/{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteDeviceCommand(id));
            
            return NoContent();
        }
    }
}
