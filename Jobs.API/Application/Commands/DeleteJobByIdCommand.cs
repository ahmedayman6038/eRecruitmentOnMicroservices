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
    public class DeleteJobByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteJobByIdCommandHandler : IRequestHandler<DeleteJobByIdCommand, Response<int>>
        {
            private readonly IJobRepository _jobRepository;
            public DeleteJobByIdCommandHandler(IJobRepository jobRepository)
            {
                _jobRepository = jobRepository;
            }
            public async Task<Response<int>> Handle(DeleteJobByIdCommand command, CancellationToken cancellationToken)
            {
                var job = await _jobRepository.GetByIdAsync(command.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                await _jobRepository.DeleteAsync(job);
                return new Response<int>(job.Id);
            }
        }
    }
}
