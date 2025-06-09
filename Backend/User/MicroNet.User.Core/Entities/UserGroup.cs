using MicroNet.User.Core.ValueObjects;

namespace MicroNet.User.Core.Entities
{
    public class UserGroup : AggregateRoot
    {
        public string UserGroupName { get; private set; }
        public bool IsActive { get; private set; }
        public Guid BranchId { get; private set; }
        public TimeRange WorkingHours { get; private set; }
        public AuditInfo AuditInfo { get; private set; } = default!;
        public bool IsDeleted { get; private set; }

        private readonly List<DaySchedule> _workingDays = new();
        public IReadOnlyCollection<DaySchedule> WorkingDays => _workingDays;

        private readonly List<UserGroupMenu> _menus = new();
        public IReadOnlyCollection<UserGroupMenu> Menus => _menus;

        private readonly List<UserSubGroupMenu> _subMenus = new();
        public IReadOnlyCollection<UserSubGroupMenu> SubMenus => _subMenus;

        private UserGroup() { } // For EF

        public UserGroup(string name, Guid branchId, TimeRange workingHours, AuditInfo auditInfo)
        {
            Id = Guid.NewGuid();
            UserGroupName = name;
            BranchId = branchId;
            WorkingHours = workingHours;
            AuditInfo = auditInfo;
            IsActive = true;
            IsDeleted = false;
        }

        public void AddWorkingDay(DayOfWeek day)
        {
            var schedule = new DaySchedule(day);
            if (!_workingDays.Contains(schedule))
                _workingDays.Add(schedule);
        }

        public void AddMenu(Guid menuId, bool isChecked, AuditInfo auditInfo)
        {
            _menus.Add(new UserGroupMenu(Id, menuId, isChecked, auditInfo));
        }

        public void AddSubMenu(Guid menuId, Guid subMenuId, bool isChecked)
        {
            _subMenus.Add(new UserSubGroupMenu(Id, menuId, subMenuId, isChecked));
        }

        public void Deactivate() => IsActive = false;
        public void Delete() => IsDeleted = true;
    }
}
