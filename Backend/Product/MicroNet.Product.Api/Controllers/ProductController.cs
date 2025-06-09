using MicroNet.Product.Application.Commands;
using MicroNet.Product.Application.Queries;
using MicroNet.Product.Core.Dtos;
using MicroNet.Shared.CQRS.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Product.Api.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public ProductController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var result = await _dispatcher.QueryAsync<GetAllProductsQuery, IEnumerable<ProductDto>>(new GetAllProductsQuery());

            return Ok(result);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var result = await _dispatcher.QueryAsync<GetProductByIdQuery, ProductDto>(new GetProductByIdQuery(id));

            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductCommand command)
        {
            //var UsrId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            //command.CreatedBy = UsrId!.ToString();
            var result = await _dispatcher.SendAsync(command);

            return Ok(result);
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in payload.");

            await _dispatcher.SendAsync(command);

            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _dispatcher.SendAsync(new DeleteProductCommand(id));

            return NoContent();
        }
    }
}
