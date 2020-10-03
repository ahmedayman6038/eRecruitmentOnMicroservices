using AutoMapper;
using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Models;
using Jobs.API.Application.Wrappers;
using MassTransit;
using MediatR;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Commands
{
    public class ApplyJobCommand : IRequest<Wrappers.Response<int>>
    {
        public int Id { get; set; }
        public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand, Wrappers.Response<int>>
        {
            private readonly IJobRepository _jobRepository;
            private readonly IBus _bus;
            private readonly IIdentityService _identityService;
            public ApplyJobCommandHandler(IJobRepository jobRepository, IBus bus, IIdentityService identityService)
            {
                _jobRepository = jobRepository;
                _bus = bus;
                _identityService = identityService;
            }

            public async Task<Wrappers.Response<int>> Handle(ApplyJobCommand request, CancellationToken cancellationToken)
            {
                var job = await _jobRepository.GetByIdAsync(request.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                Uri uri = new Uri("rabbitmq://localhost/applyQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                var message = new ApplyMessage()
                {
                    JobId = job.Id,
                    UserId = _identityService.UserId,
                    CreatedDate = DateTime.Now
                };
                await endPoint.Send(message);
                return new Wrappers.Response<int>(job.Id, $"Job Applied To User {_identityService.UserId} Is Processing.");
            }
        }
    }
}
