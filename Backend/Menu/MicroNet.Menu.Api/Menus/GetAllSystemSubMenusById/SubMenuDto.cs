namespace MicroNet.Menu.Api.Menus.GetAllSystemSubMenusById
{
    public class SubMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
    }
}
