namespace MicroNet.Menu.Api.Menus.GetAllSystemMenus
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public List<SubMenuDto> subMenuDto { get; set; } = new();
    }

    public class SubMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
    }
}
