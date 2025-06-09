using MediatR;
using MicroNet.Menu.Api.Data;
using MicroNet.Menu.Api.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace MicroNet.Menu.Api.Menus.DeleteMenu
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand>
    {
        private readonly MenuContext _context;

        public DeleteMenuCommandHandler(MenuContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
            if (menu == null)
                throw new MenuNotFoundException($"Menu with ID {request.Id} not found.");

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
