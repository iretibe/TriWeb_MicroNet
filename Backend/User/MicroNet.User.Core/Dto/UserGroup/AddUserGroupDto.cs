namespace MicroNet.User.Core.Dto.UserGroup
{
    public class AddUserGroupDto
    {
        public Guid Id { get; set; }
        public string UserGroupName { get; set; } = default!;
        public bool IsActive { get; set; } = false;
        public Guid BranchId { get; set; }
        public string StartTime { get; set; } = default!;
        public string EndTime { get; set; } = default!;
        public List<string> WorkingDays { get; set; } = default!;
        public List<AddMenuDto> Menus { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = default!;
    }

    public class AddMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid MenuId { get; set; }
        public bool IsChecked { get; set; }
        public List<AddSubMenuDto> AddSubMenuDto { get; set; } = default!;
    }

    public class AddSubMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid? MenuId { get; set; }
        public Guid SubMenuId { get; set; }
        public bool IsChecked { get; set; }
    }
}
