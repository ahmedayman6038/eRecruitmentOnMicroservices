using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Options
{
    public class RabbitMQOptions
    {
        public string BrokerName { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public string RetryCount { get; set; }
        public string Username { get; set; }
    }
}
