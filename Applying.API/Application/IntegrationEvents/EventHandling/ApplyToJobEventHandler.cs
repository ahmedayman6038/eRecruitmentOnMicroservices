using Applying.API.Application.Commands;
using Applying.API.Application.Enums;
using Applying.API.Application.IntegrationEvents.Events;
using EventBus.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Applying.API.Application.IntegrationEvents.EventHandling
{
    public class ApplyToJobEventHandler : IIntegrationEventHandler<ApplyToJobEvent>
    {
        private readonly IMediator _mediator;

        public ApplyToJobEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(ApplyToJobEvent @event)
        {
            var command = new CreateApplyCommand()
            {
                JobId = @event.JobId,
                UserId = @event.UserId,
                Status = (int)ApplyStatus.Applied,
                CreatedDate = @event.CreationDate
            };
            await _mediator.Send(command);
        }
    }
}
