using Jobs.API.Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.IntegrationEvents
{
    public interface IJobIntegrationEventService 
    {
        void Publish(IntegrationEvent @event);
    }
}
