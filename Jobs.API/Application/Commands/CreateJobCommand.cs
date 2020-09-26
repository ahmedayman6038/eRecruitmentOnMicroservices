using AutoMapper;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Models;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Commands
{
    public class CreateJobCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int CityId { get; set; }

        public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Response<int>>
        {
            private readonly IJobRepositoryAsync _jobRepository;
            private readonly IMapper _mapper;
            public CreateJobCommandHandler(IJobRepositoryAsync jobRepository, IMapper mapper)
            {
                _jobRepository = jobRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateJobCommand request, CancellationToken cancellationToken)
            {
                var job = _mapper.Map<Job>(request);
                await _jobRepository.AddAsync(job);
                return new Response<int>(job.Id);
            }
        }
    }
}
