using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Menu.Api.Menus.GetAllMenus
{
    public static class GetAllMenusEndpoint
    {
        public static void MapGetAllMenusEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/menus/GetAllMenus", async ([FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllMenusQuery());
                return Results.Ok(result);
            })
            .WithName("GetAllMenus")
            .WithTags("Menus");
        }
    }
}
