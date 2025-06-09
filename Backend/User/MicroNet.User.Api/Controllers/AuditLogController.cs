using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.User.Application.Commands.Audit;
using MicroNet.User.Application.Queries.Audit;
using MicroNet.User.Core.Dto.Audit;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.User.Api.Controllers
{
    public class AuditLogController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public AuditLogController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllAuditLogs")]
        public async Task<ActionResult<IEnumerable<AuditLogDto>>> GetAllAuditLogs()
        {
            var result = await _dispatcher.QueryAsync<GetAllAuditLogsQuery, IEnumerable<AuditLogDto>>(new GetAllAuditLogsQuery());

            return Ok(result);
        }

        [HttpGet("GetAuditLogById/{id}")]
        public async Task<ActionResult<AuditLogByIdDto>> GetAuditLogById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetAuditLogByIdQuery, AuditLogByIdDto>(new GetAuditLogByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateAuditLog")]
        public async Task<IActionResult> CreateAuditLog([FromBody] AddAuditLogCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.UserId = UsrId!.ToString();
            command.UserId = "1A9C243D-1228-4B83-AC01-D41BB8CC5541";
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }
    }
}
