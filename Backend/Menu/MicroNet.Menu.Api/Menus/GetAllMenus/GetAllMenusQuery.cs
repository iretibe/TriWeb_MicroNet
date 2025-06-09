using MediatR;

namespace MicroNet.Menu.Api.Menus.GetAllMenus
{
    public class GetAllMenusQuery : IRequest<List<MenuDto>>
    {
    }
}
