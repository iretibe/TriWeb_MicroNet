using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Menu.Api.Menus.GetMenusById
{
    public static class GetMenuByIdEndpoint
    {
        public static void MapGetMenuByIdEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/menus/GetMenuById/{menuId:guid}", async (
                Guid menuId,
                [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new GetMenuByIdQuery(menuId));
                return Results.Ok(result);
            })
            .WithName("GetMenuById")
            .WithTags("Menus");
        }
    }
}
