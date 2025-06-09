using MediatR;
using MicroNet.Menu.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroNet.Menu.Api.Menus.UpdateMenu
{
    public static class UpdateMenuEndpoint
    {
        public static void MapUpdateMenuEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPut("/api/menus/UpdateMenu/{id:guid}", async (
                Guid id,
                [FromBody] UpdateMenuCommand command,
                IMediator mediator,
                IMenuCacheService cacheService) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch");
                await mediator.Send(command);
                await cacheService.InvalidateMenuCacheAsync();
                return Results.NoContent();
            })
            .WithName("UpdateMenu")
            .WithTags("Menus");
        }
    }
}
