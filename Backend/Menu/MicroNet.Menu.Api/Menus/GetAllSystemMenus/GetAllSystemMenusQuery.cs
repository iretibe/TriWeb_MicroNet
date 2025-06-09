using MediatR;

namespace MicroNet.Menu.Api.Menus.GetAllSystemMenus
{
    public class GetAllSystemMenusQuery : IRequest<IEnumerable<MenuDto>> { }
}
