using MediatR;
using MicroNet.Menu.Api.Services;

namespace MicroNet.Menu.Api.Menus.DeleteMenu
{
    public static class DeleteMenuEndpoint
    {
        public static void MapDeleteMenuEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/menus/DeleteMenu/{id:guid}", async (
                Guid id,
                IMediator mediator,
                IMenuCacheService cacheService) =>
            {
                await mediator.Send(new DeleteMenuCommand(id));
                await cacheService.InvalidateMenuCacheAsync();
                return Results.NoContent();
            })
            .WithName("DeleteMenu")
            .WithTags("Menus");
        }
    }
}
