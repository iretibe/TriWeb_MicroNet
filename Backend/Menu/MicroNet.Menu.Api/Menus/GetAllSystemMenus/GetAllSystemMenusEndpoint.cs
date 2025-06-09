using MediatR;

namespace MicroNet.Menu.Api.Menus.GetAllSystemMenus
{
    public static class GetAllSystemMenusEndpoint
    {
        public static void MapGetAllSystemMenus(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/menus/GetAllSystemMenus", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllSystemMenusQuery());
                return Results.Ok(result);
            })
            .WithName("GetAllSystemMenus")
            .WithTags("Menus");
        }
    }
}
