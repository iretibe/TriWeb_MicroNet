namespace MicroNet.User.Core.ValueObjects
{
    public class TimeRange : ValueObject
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public TimeRange(TimeSpan start, TimeSpan end)
        {
            if (start >= end)
                throw new ArgumentException("Start time must be before end time.");

            Start = start;
            End = end;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
