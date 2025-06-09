namespace MicroNet.Device.Core.ValueObjects
{
    public class DeviceCode : ValueObject
    {
        public string Value { get; }

        public DeviceCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Device code cannot be empty", nameof(value));

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
