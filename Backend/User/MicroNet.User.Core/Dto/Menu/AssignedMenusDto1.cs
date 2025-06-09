namespace MicroNet.User.Core.Dto.Menu
{
    public class MenuDto1
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsChecked { get; set; }
        public List<SubMenuDto1> subMenuDto { get; set; } = new();
    }

    public class SubMenuDto1
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
        public bool IsChecked { get; set; }
    }

    public class AssignedMenusDto1
    {
        public string UserId { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public List<MenuDto1> AssignedMenus { get; set; } = new();
    }
}
