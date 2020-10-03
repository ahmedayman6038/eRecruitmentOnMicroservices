using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Commands
{
    public class UpdateJobCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int CityId { get; set; }

        public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Response<int>>
        {
            private readonly IJobRepository _jobRepository;
            public UpdateJobCommandHandler(IJobRepository jobRepository)
            {
                _jobRepository = jobRepository;
            }
            public async Task<Response<int>> Handle(UpdateJobCommand command, CancellationToken cancellationToken)
            {
                var job = await _jobRepository.GetByIdAsync(command.Id);

                if (job == null)
                {
                    throw new ApiException($"Job Not Found.");
                }
                else
                {
                    job.Name = command.Name;
                    job.Description = command.Description;
                    job.Requirements = command.Requirements;
                    job.CityId = command.CityId;
                    await _jobRepository.UpdateAsync(job);
                    return new Response<int>(job.Id);
                }
            }
        }
    }
}
