using Applying.API.Application.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.IntegrationEvents
{
    public interface IApplyingIntegrationEventService
    {
        void Publish(IntegrationEvent @event);
    }
}
