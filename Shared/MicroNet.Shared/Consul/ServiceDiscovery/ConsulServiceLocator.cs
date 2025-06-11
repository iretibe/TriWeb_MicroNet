using Consul;

namespace MicroNet.Shared.Consul.ServiceDiscovery
{
    // For discovering services
    public class ConsulServiceLocator
    {
        private readonly IConsulClient _consulClient;

        public ConsulServiceLocator(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task<Uri?> GetServiceUriAsync(string serviceName)
        {
            var services = await _consulClient.Agent.Services();
            var service = services.Response.Values
                .Where(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(_ => Guid.NewGuid()) // Load balancing-ish
                .FirstOrDefault();

            return service != null
                ? new Uri($"http://{service.Address}:{service.Port}")
                : null;
        }
    }
}
