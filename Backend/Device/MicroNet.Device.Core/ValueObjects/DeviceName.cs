namespace MicroNet.Device.Core.ValueObjects
{
    public class DeviceName : ValueObject
    {
        public string Value { get; }

        public DeviceName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Device name cannot be empty", nameof(value));

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
