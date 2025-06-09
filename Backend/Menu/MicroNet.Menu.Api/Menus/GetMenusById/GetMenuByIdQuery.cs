using MediatR;

namespace MicroNet.Menu.Api.Menus.GetMenusById
{
    public class GetMenuByIdQuery : IRequest<MenuDto>
    {
        public Guid MenuId { get; set; }

        public GetMenuByIdQuery(Guid menuId)
        {
            MenuId = menuId;
        }
    }
}
