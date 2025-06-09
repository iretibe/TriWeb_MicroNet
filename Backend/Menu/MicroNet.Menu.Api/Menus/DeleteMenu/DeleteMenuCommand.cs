using MediatR;

namespace MicroNet.Menu.Api.Menus.DeleteMenu
{
    public class DeleteMenuCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteMenuCommand(Guid id)
        {
            Id = id;
        }
    }
}
