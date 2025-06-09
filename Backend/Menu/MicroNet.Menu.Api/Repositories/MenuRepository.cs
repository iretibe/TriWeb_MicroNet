using MicroNet.Menu.Api.Data;
using MicroNet.Menu.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MenuContext _context;

        public MenuRepository(MenuContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entities.Menu>> GetAllMenuAsync()
        {
            return await _context.Menus
                .Include(m => m.SubMenus).OrderBy(m => m.MenuOrderId).ToListAsync();
        }

        public async Task<IEnumerable<MenuDto>> GetAllSystemMenusAsync()
        {
            // Fetch root menus
            var query = await (from m in _context.Menus.OrderBy(m => m.MenuOrderId)
                               where m.ParentId == Guid.Parse("00000000-0000-0000-0000-000000000000")
                               select new MenuDto
                               {
                                   Id = m.Id,
                                   Name = m.Name,
                               }).ToListAsync();

            if (query != null && query.Any())
            {
                foreach (var item in query)
                {
                    // Fetch submenus for each root menu
                    var subMenus = await (from sm in _context.Menus.OrderBy(sm => sm.MenuOrderId)
                                          where sm.ParentId == item.Id
                                          select new SubMenuDto
                                          {
                                              Id = sm.Id,
                                              Name = sm.Name,
                                              MenuId = sm.ParentId
                                          }).ToListAsync();

                    // Assign submenus to the corresponding MenuDto
                    item.subMenuDto = subMenus;
                }
            }

            return query!;
        }

        public async Task<IEnumerable<SubMenuDto>> GetAllSystemSubMenusByIdAsync(Guid menuId)
        {
            var query = await (from sm in _context.Menus.OrderBy(sm => sm.MenuOrderId)
                               where sm.ParentId == menuId
                               select new SubMenuDto
                               {
                                   Id = sm.Id,
                                   Name = sm.Name,
                                   MenuId = sm.ParentId
                               }).ToListAsync();

            return query;
        }

        public async Task<Entities.Menu> GetMenusByIdAsync(Guid menuId)
        {
            var query = await _context
                .Menus
                .Include(m => m.SubMenus)
                .OrderBy(m => m.MenuOrderId)
                .SingleOrDefaultAsync(m => m.Id == menuId);

            return query!;
        }
    }
}
