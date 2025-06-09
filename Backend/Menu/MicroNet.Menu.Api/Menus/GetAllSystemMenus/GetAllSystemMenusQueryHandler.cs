using MediatR;
using MicroNet.Menu.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.GetAllSystemMenus
{
    public class GetAllSystemMenusQueryHandler : IRequestHandler<GetAllSystemMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly MenuContext _context;

        public GetAllSystemMenusQueryHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetAllSystemMenusQuery request, CancellationToken cancellationToken)
        {
            var rootMenus = await (from m in _context.Menus.OrderBy(m => m.MenuOrderId)
                                   where m.ParentId == Guid.Empty
                                   select new MenuDto
                                   {
                                       Id = m.Id,
                                       Name = m.Name,
                                   }).ToListAsync(cancellationToken);

            foreach (var menu in rootMenus)
            {
                var subMenus = await (from sm in _context.Menus.OrderBy(sm => sm.MenuOrderId)
                                      where sm.ParentId == menu.Id
                                      select new SubMenuDto
                                      {
                                          Id = sm.Id,
                                          Name = sm.Name,
                                          MenuId = sm.ParentId
                                      }).ToListAsync(cancellationToken);

                menu.subMenuDto = subMenus;
            }

            return rootMenus;
        }
    }
}
