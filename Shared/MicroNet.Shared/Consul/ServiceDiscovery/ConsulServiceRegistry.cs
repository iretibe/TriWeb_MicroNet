using Consul;

namespace MicroNet.Shared.Consul.ServiceDiscovery
{
    // For registering the service
    public class ConsulServiceRegistry
    {
        private readonly IConsulClient _consulClient;
        private readonly string _serviceId;
        private readonly AgentServiceRegistration _registration;

        public ConsulServiceRegistry(IConsulClient consulClient, string serviceName, string address, int port)
        {
            _consulClient = consulClient;
            _serviceId = $"{serviceName}-{Guid.NewGuid()}";

            _registration = new AgentServiceRegistration
            {
                ID = _serviceId,
                Name = serviceName,
                Address = address,
                Port = port,
                Tags = new[] { serviceName },
                Check = new AgentServiceCheck
                {
                    HTTP = $"https://{address}:{port}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
                }
            };
        }

        public async Task RegisterAsync()
        {
            await _consulClient.Agent.ServiceRegister(_registration);
        }

        public async Task DeregisterAsync()
        {
            await _consulClient.Agent.ServiceDeregister(_serviceId);
        }
    }
}
