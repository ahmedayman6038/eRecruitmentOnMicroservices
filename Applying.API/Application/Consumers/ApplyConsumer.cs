using Applying.API.Application.Commands;
using Applying.API.Application.Enums;
using Applying.API.Application.Models;
using MassTransit;
using MediatR;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applying.API.Application.Consumers
{
    public class ApplyConsumer : IConsumer<ApplyMessage>
    {
        private readonly IMediator _mediator;

        public ApplyConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ApplyMessage> context)
        {
            var data = context.Message;
            var command = new CreateApplyCommand();
            command.JobId = data.JobId;
            command.UserId = data.UserId;
            command.Status = (int)ApplyStatus.Applied;
            command.CreatedDate = data.CreatedDate;
            await _mediator.Send(command);
        }
    }
}
