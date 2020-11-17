using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ.Options;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRabbitMQConnection(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var retryCount = 5;

                var factory = new ConnectionFactory
                {
                    HostName = options.Host,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(options.Username))
                {
                    factory.UserName = options.Username;
                }

                if (!string.IsNullOrEmpty(options.Password))
                {
                    factory.Password = options.Password;
                }

                if (!string.IsNullOrEmpty(options.RetryCount))
                {
                    retryCount = int.Parse(options.RetryCount);
                }

                return new DefaultRabbitMQPersistentConnection(factory, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddRabbitMQRegistration(this IServiceCollection services, RabbitMQOptions options)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var lifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var brokerName = options.BrokerName;
                var queueName = options.QueueName;
                var retryCount = 5;

                if (!string.IsNullOrEmpty(options.RetryCount))
                {
                    retryCount = int.Parse(options.RetryCount);
                }

                return new EventBusRabbitMQ(rabbitMqPersistentConnection,
                    lifetimeScope,
                    eventBusSubscriptionsManager,
                    brokerName,
                    queueName,
                    retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
