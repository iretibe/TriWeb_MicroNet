using MicroNet.Shared;

namespace MicroNet.Menu.Api.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public Guid? ParentId { get; set; }
        public int MenuOrderId { get; set; }        
        public string UId { get; set; } = default!;
        public virtual ICollection<Menu> SubMenus { get; set; } = default!;
    }
}
