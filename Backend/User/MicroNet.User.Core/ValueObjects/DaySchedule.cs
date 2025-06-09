using Microsoft.EntityFrameworkCore;

namespace MicroNet.User.Core.ValueObjects
{
    [Owned]
    public class DaySchedule : ValueObject
    {
        public DayOfWeek Day { get; }

        // Required by EF Core
        private DaySchedule() { }

        public DaySchedule(DayOfWeek day)
        {
            Day = day;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Day;
        }
    }
}
