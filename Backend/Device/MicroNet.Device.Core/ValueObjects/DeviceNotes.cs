namespace MicroNet.Device.Core.ValueObjects
{
    public class DeviceNotes : ValueObject
    {
        public string Value { get; }

        public DeviceNotes(string value)
        {
            Value = value ?? string.Empty;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
