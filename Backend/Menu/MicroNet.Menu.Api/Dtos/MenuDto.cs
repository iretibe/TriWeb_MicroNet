namespace MicroNet.Menu.Api.Dtos
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsChecked { get; set; }
        public List<SubMenuDto> subMenuDto { get; set; } = default!;
    }

    public class SubMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
        public bool IsChecked { get; set; }
    }

    //public class AssignedMenusDto
    //{
    //    public string UserId { get; set; } = default!;
    //    public string CreatedBy { get; set; } = default!;
    //    public List<MenuDto> AssignedMenus { get; set; } = default!;
    //}
}
