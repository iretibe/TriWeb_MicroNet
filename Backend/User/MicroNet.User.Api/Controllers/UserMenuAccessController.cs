using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.Access;
using MicroNet.User.Application.Queries.Access;
using MicroNet.User.Core.Dto.Menu;
using MicroNet.User.Core.Dto.UserGroup;
using MicroNet.User.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.User.Api.Controllers
{
    //Replaces Menu controller othe old API
    public class UserMenuAccessController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public UserMenuAccessController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllAssignedSystemMenusByUserId/{userId}")]
        public async Task<ActionResult<AssignedMenusDto1>> GetAllAssignedSystemMenusByUserId(string userId)
        {
            var result = await _dispatcher.QueryAsync<GetAllAssignedSystemMenusByUserIdQuery, AssignedMenusDto1>(new GetAllAssignedSystemMenusByUserIdQuery(userId));

            return Ok(result);
        }

        [HttpGet("GetAllMenuLists")]
        public async Task<ActionResult<IEnumerable<MenuEntityDto>>> GetAllMenuLists()
        {
            var result = await _dispatcher.QueryAsync<GetAllMenuListQuery, IEnumerable<MenuEntityDto>>(new GetAllMenuListQuery());

            return Ok(result);
        }

        [HttpGet("GetMenusForUser/{userId}")]
        public async Task<ActionResult<MenuEntityDto>> GetMenusForUser(string userId)
        {
            var result = await _dispatcher.QueryAsync<GetMenusForUserQuery, IEnumerable<MenuEntityDto>>(new GetMenusForUserQuery(userId));

            return Ok(result);
        }

        [HttpGet("GetUserSystemMenusForUpdate/{userId}")]
        public async Task<ActionResult<IEnumerable<AssignedMenusForUpdateDto>>> GetUserSystemMenusForUpdate(string userId)
        {
            var result = await _dispatcher.QueryAsync<GetUserSystemMenusForUpdateQuery, AssignedMenusForUpdateDto>(new GetUserSystemMenusForUpdateQuery(userId));

            return Ok(result);
        }

        [HttpPost("CreateBulkUserMenuAccess")]
        public async Task<ActionResult<UserMenuAccess>> CreateBulkUserMenuAccess([FromBody] List<AddBulkUserMenuAccessCommand> command)
        {
            var results = new List<UserMenuAccess>();

            foreach (var entity in command)
            {
                //entity.Entity.Id = Guid.NewGuid();
                //entity.Entity.AuditInfo.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
                var result = await _dispatcher.SendAsync(entity);
                results.Add(result);
            }
            
            return Ok(results);
        }

        [HttpPost("CreateUserMenuAccess")]
        public async Task<ActionResult<AddUserGroupDto>> CreateUserMenuAccess([FromBody] AddUserMenuAccessCommand command)
        {
            //command.Id = Guid.NewGuid();
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId.ToString();
            //command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPost("CreateAssignMenusToUser")]
        public async Task<ActionResult<AddUserGroupDto>> CreateAssignMenusToUser([FromBody] AssignMenusToUserCommand command)
        {
            //command.Id = Guid.NewGuid();
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId.ToString();
            //command.CreatedBy = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);
            return Ok(result);
        }

        [HttpPut("UpdateAssignMenusToUser")]
        public async Task<ActionResult> UpdateAssignMenusToUser([FromBody] UpdateAssignMenusToUserCommand command)
        {
            await _dispatcher.SendAsync(command);

            return NoContent();
        }
    }
}
