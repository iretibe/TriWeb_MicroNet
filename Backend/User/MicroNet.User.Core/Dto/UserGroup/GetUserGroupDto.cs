namespace MicroNet.User.Core.Dto.UserGroup
{
    public class GetUserGroupDto
    {
        public Guid Id { get; set; }
        public string UserGroupName { get; set; } = default!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = default!;
        public List<GetWorkingDayDto> WorkingDaysDto { get; set; } = default!;
        public List<GetMenuDto> MenuDto { get; set; } = default!;
    }

    public class GetWorkingDayDto
    {
        //public DayOfWeek DayOfWeek { get; set; }
        public int DayOfWeek { get; set; }
        public string DayName { get; set; } = default!;
    }

    public class GetMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid MenuId { get; set; }
        public string MenuName { get; set; } = default!;
        public bool IsChecked { get; set; }
        public List<GetSubMenuDto> SubMenuDto { get; set; } = default!;
    }

    public class GetSubMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid? MenuId { get; set; }
        public string MenuName { get; set; } = default!;
        public Guid SubMenuId { get; set; }
        public string SubMenuName { get; set; } = default!;
        public bool IsChecked { get; set; }
    }
}
