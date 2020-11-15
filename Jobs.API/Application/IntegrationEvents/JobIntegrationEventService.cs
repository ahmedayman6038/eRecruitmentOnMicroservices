using Jobs.API.Application.IntegrationEvents.Events;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.API.Application.IntegrationEvents
{
    public class JobIntegrationEventService : IJobIntegrationEventService
    {
        public RabbitMqConfiguration _rabbitMqConfiguration { get; }
        public JobIntegrationEventService(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _rabbitMqConfiguration = rabbitMqOptions.Value;
        }

        public void Publish(IntegrationEvent @event)
        {
            var factory = new ConnectionFactory() { 
                HostName = _rabbitMqConfiguration.HostName, 
                UserName = _rabbitMqConfiguration.UserName, 
                Password = _rabbitMqConfiguration.Password
            };
            var eventName = @event.GetType().Name;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);
            }
        }
    }
}
