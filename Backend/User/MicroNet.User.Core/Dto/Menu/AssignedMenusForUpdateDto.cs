namespace MicroNet.User.Core.Dto.Menu
{
    public class MenuForUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsChecked { get; set; }
        public List<SubMenuForUpdateDto> SubMenuDto { get; set; } = new();
    }

    public class SubMenuForUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
        public bool IsChecked { get; set; }
    }

    public class AssignedMenusForUpdateDto
    {
        public string UserId { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public List<MenuForUpdateDto> AssignedMenus { get; set; } = new();
    }
}
