using MediatR;
using MicroNet.Menu.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.GetAllSystemSubMenusById
{
    public class GetAllSystemSubMenusByIdQueryHandler : IRequestHandler<GetAllSystemSubMenusByIdQuery, IEnumerable<SubMenuDto>>
    {
        private readonly MenuContext _context;

        public GetAllSystemSubMenusByIdQueryHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubMenuDto>> Handle(GetAllSystemSubMenusByIdQuery request, CancellationToken cancellationToken)
        {
            return await (from sm in _context.Menus.OrderBy(sm => sm.MenuOrderId)
                          where sm.ParentId == request.MenuId
                          select new SubMenuDto
                          {
                              Id = sm.Id,
                              Name = sm.Name,
                              MenuId = sm.ParentId
                          }).ToListAsync(cancellationToken);
        }
    }

}
