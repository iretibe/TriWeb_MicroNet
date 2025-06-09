namespace MicroNet.Device.Application.Exceptions
{
    public class DeviceAlreadyExistsException : AppException
    {
        public DeviceAlreadyExistsException(Guid id) : base($"Device with ID: '{id}' already exists!")
        {
        }
    }
}
