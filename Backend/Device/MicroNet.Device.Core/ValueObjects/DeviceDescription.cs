namespace MicroNet.Device.Core.ValueObjects
{
    public class DeviceDescription : ValueObject
    {
        public string Value { get; }

        public DeviceDescription(string value)
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
