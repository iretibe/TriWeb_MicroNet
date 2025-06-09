﻿namespace MicroNet.Shared.Messaging.RabbitMq
{
    public class RabbitMQSettings
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string VirtualHost { get; set; } = default!;
        public string Exchange { get; set; } = default!;
        public string Queue { get; set; } = default!;
        public string RoutingKey { get; set; } = default!;
    }
}
