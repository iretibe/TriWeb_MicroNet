namespace MicroNet.User.Core.Dto.UserGroup
{
    public class UpdateUserGroupDto
    {
        public Guid Id { get; set; }
        public string UserGroupName { get; set; } = default!;
        public bool IsActive { get; set; } = false;
        public Guid BranchId { get; set; }
        public string StartTime { get; set; } = default!;
        public string EndTime { get; set; } = default!;
        //public List<string> WorkingDays { get; set; }
        public List<UpdateWorkingDayDto> WorkingDays { get; set; } = default!;
        public List<UpdateMenuDto> Menus { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = default!;
    }

    public class UpdateMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid MenuId { get; set; }
        public bool IsChecked { get; set; }
        public List<UpdateSubMenuDto> UpdSubMenuDto { get; set; } = default!;
    }

    public class UpdateSubMenuDto
    {
        public Guid UserGroupId { get; set; }
        public Guid? MenuId { get; set; }
        public Guid SubMenuId { get; set; }
        public bool IsChecked { get; set; }
    }

    public class UpdateWorkingDayDto
    {
        public Guid UserGroupId { get; set; }
        //public DayOfWeek DayOfWeek { get; set; }
        public int DayOfWeek { get; set; }
    }
}
