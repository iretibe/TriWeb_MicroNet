namespace MicroNet.User.Core.Entities
{
    public class UserGroupWorkingDay : AggregateRoot
    {
        public Guid UserGroupId { get; private set; }

        //DayOfWeek as int: 0 = Sunday, 6 = Saturday
        public int DayOfWeek { get; private set; }

        public string CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private UserGroupWorkingDay() { }

        public UserGroupWorkingDay(Guid userGroupId, int dayOfWeek, string createdBy)
        {
            if (dayOfWeek < 0 || dayOfWeek > 6)
                throw new ArgumentOutOfRangeException(nameof(dayOfWeek), "DayOfWeek must be between 0 (Sunday) and 6 (Saturday).");

            UserGroupId = userGroupId;
            DayOfWeek = dayOfWeek;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
