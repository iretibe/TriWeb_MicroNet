using MediatR;
using MicroNet.Menu.Api.Data;
using MicroNet.Menu.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.UpdateMenu
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand>
    {
        private readonly MenuContext _context;

        public UpdateMenuCommandHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            if (menu == null)
                throw new MenuNotFoundException($"Menu with ID {request.Id} not found.");

            menu.Name = request.Name;
            menu.Controller = request.Controller;
            menu.Action = request.Action;
            menu.Icon = request.Icon;
            menu.ParentId = request.ParentId;
            menu.MenuOrderId = request.MenuOrderId;
            menu.UId = request.UId;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
