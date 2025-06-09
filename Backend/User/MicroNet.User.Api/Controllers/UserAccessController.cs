using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.UserGroup;
using MicroNet.User.Application.Queries.UserGroup;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.User.Api.Controllers
{
    public class UserAccessController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public UserAccessController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetUserGroupById/{Id}")]
        public async Task<ActionResult<GetUserGroupDto>> GetUserGroupById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetUserGroupByIdQuery, GetUserGroupDto>(new GetUserGroupByIdQuery(id));

            return Ok(result);
        }

        [HttpGet("GetUserGroupByName/{userGroupName}/{branchId}")]
        public async Task<ActionResult<UserGroup>> GetUserGroupByName(string userGroupName, Guid branchId)
        {
            var result = await _dispatcher.QueryAsync<GetUserGroupByNameQuery, UserGroup>(new GetUserGroupByNameQuery(userGroupName, branchId));

            return Ok(result);
        }

        [HttpGet("GetAllUserGroups")]
        public async Task<ActionResult<IEnumerable<UserGroupDto>>> GetAllUserGroups()
        {
            var result = await _dispatcher.QueryAsync<GetAllUserGroupsQuery, IEnumerable<UserGroupDto>>(new GetAllUserGroupsQuery());

            return Ok(result);
        }

        [HttpPost("CreateUserGroups")]
        public async Task<ActionResult<AddUserGroupDto>> CreateUserGroups([FromBody] AddUserGroupsCommand command)
        {
            command.Id = Guid.NewGuid();
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId.ToString();
            command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPut("UpdateUserGroup/{id}")]
        public async Task<ActionResult> UpdateUserGroup(Guid id, [FromBody] UpdateUserGroupMenusCommand command)
        {
            if (id != command.Entity.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteUserGroup/{id}")]
        public async Task<ActionResult> DeleteUserGroup(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteUserGroupCommand(id));

            return NoContent();
        }
    }
}
