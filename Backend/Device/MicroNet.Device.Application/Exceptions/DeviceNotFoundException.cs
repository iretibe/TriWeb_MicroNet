namespace MicroNet.Device.Application.Exceptions
{
    public class DeviceNotFoundException : AppException
    {
        public DeviceNotFoundException(Guid id) : base($"Device with ID: '{id}' is not found!")
        {
        }
    }
}
