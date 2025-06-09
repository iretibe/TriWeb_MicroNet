using MediatR;
using MicroNet.Menu.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Menu.Api.Menus.CreateMenu
{
    public static class CreateMenuEndpoint
    {
        public static void MapCreateMenuEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/menus/CreateMenu", async (
                [FromBody] CreateMenuCommand command,
                IMediator mediator, IMenuCacheService cacheService) =>
            {
                var id = await mediator.Send(command);
                await cacheService.InvalidateMenuCacheAsync();
                return Results.Created($"/api/menus/{id}", new { id });
            })
            .WithName("CreateMenu")
            .WithTags("Menus");
        }
    }
}
