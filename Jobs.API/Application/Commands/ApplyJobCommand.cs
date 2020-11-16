using AutoMapper;
using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Entities;
using Jobs.API.Application.Wrappers;
using MediatR;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Jobs.API.Application.IntegrationEvents;
using Jobs.API.Application.IntegrationEvents.Events;
using EventBus.Abstractions;

namespace Jobs.API.Application.Commands
{
    public class ApplyJobCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand, Response<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEventBus _eventBus;
            private readonly IIdentityService _identityService;

            public ApplyJobCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus, IIdentityService identityService)
            {
                _unitOfWork = unitOfWork;
                _eventBus = eventBus;
                _identityService = identityService;
            }

            public async Task<Response<int>> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
            {
                var job = await _unitOfWork.Jobs.GetByIdAsync(request.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                var eventMessage = new ApplyToJobEvent(job.Id, _identityService.UserId);
                try
                {
                    _eventBus.Publish(eventMessage);
                }
                catch(Exception ex)
                {
                    throw new ApiException($"ERROR Publishing integration event: {eventMessage.Id} from {Program.AppName}");
                }
                return new Response<int>(job.Id, $"Job Applied To User {_identityService.UserId} Is Processing.");
            }
        }
    }
}
