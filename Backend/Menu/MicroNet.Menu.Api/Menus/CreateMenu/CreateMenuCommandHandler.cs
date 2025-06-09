using MediatR;
using MicroNet.Menu.Api.Data;

namespace MicroNet.Menu.Api.Menus.CreateMenu
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Guid>
    {
        private readonly MenuContext _context;

        public CreateMenuCommandHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new Entities.Menu
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Controller = request.Controller,
                Action = request.Action,
                Icon = request.Icon,
                ParentId = request.ParentId,
                MenuOrderId = request.MenuOrderId,
                UId = request.UId,
                SubMenus = new List<Entities.Menu>()
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync(cancellationToken);
            return menu.Id;
        }
    }
}
