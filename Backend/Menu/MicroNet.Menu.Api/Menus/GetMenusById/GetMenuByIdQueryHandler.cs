using MediatR;
using MicroNet.Menu.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.GetMenusById
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
    {
        private readonly MenuContext _context;

        public GetMenuByIdQueryHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus
                .Include(m => m.SubMenus)
                .OrderBy(m => m.MenuOrderId)
                .SingleOrDefaultAsync(m => m.Id == request.MenuId, cancellationToken);

            if (menu == null)
                throw new KeyNotFoundException($"Menu with ID {request.MenuId} not found.");

            return MapToDto(menu);
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
