using MicroNet.Shared.CQRS.Dispatchers;
using MicroNet.System.Application.Commands;
using MicroNet.System.Application.Queries;
using MicroNet.System.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.System.Api.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public CompanyController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllCompanies")]
        public async Task<ActionResult<IEnumerable<CompanySetup>>> GetAllCompanies()
        {
            var result = await _dispatcher.QueryAsync<GetAllCompaniesQuery, IEnumerable<CompanySetup>>(new GetAllCompaniesQuery());

            return Ok(result);
        }

        [HttpGet("GetCompanyById/{id}")]
        public async Task<ActionResult<CompanySetup>> GetCompanyById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetCompanyByIdQuery, CompanySetup>(new GetCompanyByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanySetupCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateCompany/{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanySetupCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteCompanySetupCommand(id));

            return NoContent();
        }
    }
}
