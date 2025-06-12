using Consul;
using Microsoft.Extensions.Configuration;

namespace MicroNet.Shared.Consul.ServiceDiscovery
{
    // For registering the service
    public class ConsulServiceRegistry
    {
        private readonly IConsulClient _consulClient;
        private readonly string _serviceId;
        private readonly AgentServiceRegistration _registration;

        public ConsulServiceRegistry(IConfiguration configuration)
        {
            var consulConfig = configuration.GetSection("consul");
            var fabioConfig = configuration.GetSection("fabio");

            _serviceId = consulConfig["ServiceId"] ?? $"{consulConfig["ServiceName"]}-{Guid.NewGuid()}";

            string address = consulConfig["Host"] ?? "localhost";
            int port = int.Parse(consulConfig["Port"] ?? "80");

            var tags = new List<string>();

            // Add standard tags
            var rawTags = consulConfig.GetSection("ServiceTags").Get<string[]>() ?? Array.Empty<string>();
            tags.AddRange(rawTags);

            // Fabio specific: add urlprefix tag for route discovery
            string fabioPrefix = $"urlprefix-/{rawTags.FirstOrDefault() ?? "default"} strip=/{rawTags.FirstOrDefault() ?? "default"}";
            tags.Add(fabioPrefix);

            _consulClient = new ConsulClient(cfg =>
            {
                cfg.Address = new Uri(consulConfig["Address"] ?? "http://localhost:8500");
            });

            _registration = new AgentServiceRegistration
            {
                ID = _serviceId,
                Name = consulConfig["ServiceName"]!,
                Address = address,
                Port = port,
                Tags = tags.ToArray(),
                Check = new AgentServiceCheck
                {
                    HTTP = $"https://{address}:{port}/health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
                }
            };
        }

        //public ConsulServiceRegistry(IConsulClient consulClient, string serviceName, string address, int port)
        //{
        //    _consulClient = consulClient;
        //    _serviceId = $"{serviceName}-{Guid.NewGuid()}";

        //    _registration = new AgentServiceRegistration
        //    {
        //        ID = _serviceId,
        //        Name = serviceName,
        //        Address = address,
        //        Port = port,
        //        Tags = new[] { serviceName },
        //        Check = new AgentServiceCheck
        //        {
        //            HTTP = $"https://{address}:{port}/health",
        //            Interval = TimeSpan.FromSeconds(10),
        //            Timeout = TimeSpan.FromSeconds(5),
        //            DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1)
        //        }
        //    };
        //}

        public async Task RegisterAsync()
        {
            await _consulClient.Agent.ServiceDeregister(_registration.ID); // clean stale
            await _consulClient.Agent.ServiceRegister(_registration);
        }

        public async Task DeregisterAsync()
        {
            await _consulClient.Agent.ServiceDeregister(_serviceId);
        }
    }
}
