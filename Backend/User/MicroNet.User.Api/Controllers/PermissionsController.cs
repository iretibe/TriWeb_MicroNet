using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.Permission;
using MicroNet.User.Application.Queries.Permission;
using MicroNet.User.Core.Dto.Permission;
using MicroNet.User.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.User.Api.Controllers
{
    public class PermissionsController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public PermissionsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllPermissions")]
        public async Task<ActionResult<IEnumerable<UserPermissionDto>>> GetAllPermissions()
        {
            var result = await _dispatcher.QueryAsync<GetAllUserPermissionsQuery, IEnumerable<UserPermissionDto>>(new GetAllUserPermissionsQuery());

            return Ok(result);
        }

        [HttpGet("GetPermissionById/{id}")]
        public async Task<ActionResult<UserPermission>> GetPermissionById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetUserPermissionsByIdQuery, UserPermission>(new GetUserPermissionsByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreatePermission")]
        public async Task<IActionResult> CreatePermission([FromBody] CreateUserPermissionCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdatePermission/{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] UpdateUserPermissionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeletePermission/{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteUserPermissionCommand(id));

            return NoContent();
        }
    }
}
