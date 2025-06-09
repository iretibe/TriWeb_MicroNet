namespace MicroNet.Device.Application.Exceptions
{
    public class DeviceNameNotFoundException : AppException
    {
        public DeviceNameNotFoundException(string name) : base($"Device with Name: '{name}' already exists!")
        {
        }
    }
}
