using MicroNet.Device.Core.Clients;
using MicroNet.Shared.Consul.ServiceDiscovery;

namespace MicroNet.Device.Infrastructure.Clients
{
    public class DeviceServiceClient : IDeviceServiceClient
    {
        private readonly HttpClient _client;

        public DeviceServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetAllDevicesAsync()
        {
            var response = await _client.GetAsync("/api/devices/GetAllDevices");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
