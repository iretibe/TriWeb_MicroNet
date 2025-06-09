using MicroNet.Device.Core.Entities;

namespace MicroNet.Device.Core.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Entities.Device>> GetDevicesAsync();
        Task<Entities.Device> GetDeviceByIdAsync(AggregateId deviceId);
        Task<string> GetDeviceNameByIdAsync(AggregateId deviceId);
        Task<string> GetDeviceByNameAsync(string deviceName);
        Task AddDeviceAsync(Entities.Device device);
        Task DeleteDeviceAsync(AggregateId deviceId);
        Task UpdateDeviceAsync(Entities.Device device);
    }
}
