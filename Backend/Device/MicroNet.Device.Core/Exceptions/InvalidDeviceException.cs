namespace MicroNet.Device.Core.Exceptions
{
    public class InvalidDeviceException : DomainException
    {
        public Guid Id { get; }

        public InvalidDeviceException(Guid id) : base($"Invalid Aggregate ID {id}")
        {
            Id = id;
        }
    }
}
