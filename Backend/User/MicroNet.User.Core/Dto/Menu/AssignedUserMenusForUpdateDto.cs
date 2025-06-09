namespace MicroNet.User.Core.Dto.Menu
{
    public class AssignedUserMenusForUpdateDto
    {
        public string UserId { get; set; } = default!;
        public List<MenuUserForUpdateDto> AssignedMenus { get; set; } = new();
    }

    public class MenuUserForUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsChecked { get; set; }
        public List<SubUserMenuForUpdateDto> SubMenuDto { get; set; } = new();
    }

    public class SubUserMenuForUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? MenuId { get; set; }
        public bool IsChecked { get; set; }
    }
}
