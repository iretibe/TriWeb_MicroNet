using Consul;

namespace MicroNet.Shared.Consul.ServiceDiscovery
{
    // For registering the service
    public class ConsulServiceRegistry
    {
        private readonly IConsulClient _consulClient;
        private readonly string _serviceId;

        public ConsulServiceRegistry(IConsulClient consulClient, string serviceName, string address, int port)
        {
            _consulClient = consulClient;
            _serviceId = $"{serviceName}-{Guid.NewGuid()}";

            var registration = new AgentServiceRegistration
            {
                ID = _serviceId,
                Name = serviceName,
                Address = address,
                Port = port,
                Tags = new[] { serviceName },
                Check = new AgentServiceCheck
                {
                    HTTP = $"http://{address}:{port}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
                }
            };

            _consulClient.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
        }

        public async Task DeregisterAsync()
        {
            await _consulClient.Agent.ServiceDeregister(_serviceId);
        }
    }
}
