using MediatR;
using MicroNet.Menu.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.GetAllMenus
{
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<MenuDto>>
    {
        private readonly MenuContext _context;

        public GetAllMenusQueryHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            var menus = await _context.Menus
                .Include(m => m.SubMenus)
                .Where(m => m.ParentId == null)
                .OrderBy(m => m.MenuOrderId)
                .ToListAsync(cancellationToken);

            return menus.Select(m => MapToDto(m)).ToList();
        }

        private MenuDto MapToDto(Entities.Menu menu)
        {
            return new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                Controller = menu.Controller,
                Action = menu.Action,
                Icon = menu.Icon,
                ParentId = menu.ParentId,
                MenuOrderId = menu.MenuOrderId,
                UId = menu.UId,
                SubMenus = menu.SubMenus
                    .OrderBy(sm => sm.MenuOrderId)
                    .Select(MapToDto)
                    .ToList()
            };
        }
    }
}
