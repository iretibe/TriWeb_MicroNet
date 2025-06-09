using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.User;
using MicroNet.User.Application.Queries.User;
using MicroNet.User.Core.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroNet.User.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public UsersController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserRecordsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserRecordsDto>>> GetAllUsers()
        {
            var result = await _dispatcher.QueryAsync<GetAllUsersQuery, IEnumerable<UserRecordsDto>>(new GetAllUsersQuery());
            
            return Ok(result);
        }

        [HttpGet("GetAllSysAdminUsers")]
        [ProducesResponseType(typeof(IEnumerable<SysAdminRecordsDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<SysAdminRecordsDto>>> GetAllSysAdminUsers()
        {
            var result = await _dispatcher.QueryAsync<GetAllSysAdminUsersQuery, IEnumerable<SysAdminRecordsDto>>(new GetAllSysAdminUsersQuery());
            
            return Ok(result);
        }

        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreateBy = Guid.Parse(UsrId!);
            command.CreateBy = Guid.Parse("1A9C243D-1228-4B83-AC01-D41BB8CC5541");
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteUser")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCustomer(string id)
        {
            await _dispatcher.SendAsync(new DeleteUserCommand(id));

            return NoContent();
        }
    }
}
