namespace MicroNet.User.Core.Dto.Menu
{
    public class MenuEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public Guid? ParentId { get; set; }
        public int MenuOrderId { get; set; }
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string UId { get; set; } = default!;
        public virtual ICollection<MenuEntityDto> SubMenus { get; set; } = default!;
    }
}
