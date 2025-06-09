using MediatR;

namespace MicroNet.Menu.Api.Menus.GetAllSystemSubMenusById
{
    public static class GetAllSystemSubMenusByIdEndpoint
    {
        public static void MapGetAllSystemSubMenusById(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/menus/GetAllSystemSubMenusById/{menuId:guid}", async (Guid menuId, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllSystemSubMenusByIdQuery(menuId));
                return Results.Ok(result);
            })
            .WithName("GetAllSystemSubMenusById")
            .WithTags("Menus");
        }
    }
}
