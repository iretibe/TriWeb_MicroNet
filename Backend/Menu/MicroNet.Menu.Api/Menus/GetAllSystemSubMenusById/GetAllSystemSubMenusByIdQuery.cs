using MediatR;

namespace MicroNet.Menu.Api.Menus.GetAllSystemSubMenusById
{
    public class GetAllSystemSubMenusByIdQuery : IRequest<IEnumerable<SubMenuDto>>
    {
        public Guid MenuId { get; set; }
        public GetAllSystemSubMenusByIdQuery(Guid menuId) => MenuId = menuId;
    }
}
