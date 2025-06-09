using MediatR;

namespace MicroNet.Menu.Api.Menus.CreateMenu
{
    public class CreateMenuCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public Guid? ParentId { get; set; }
        public int MenuOrderId { get; set; }
        public string UId { get; set; } = default!;
    }
}
