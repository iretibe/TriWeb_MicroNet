namespace MicroNet.Device.Core.Clients
{
    public interface IDeviceServiceClient
    {
        Task<string> GetAllDevicesAsync();
    }
}
