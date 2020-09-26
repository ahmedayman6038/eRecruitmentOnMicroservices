using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Models;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Queries
{
    public class GetJobByIdQuery : IRequest<Response<Job>>
    {
        public int Id { get; set; }
        public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Response<Job>>
        {
            private readonly IJobRepositoryAsync _jobRepository;
            public GetJobByIdQueryHandler(IJobRepositoryAsync jobRepository)
            {
                _jobRepository = jobRepository;
            }
            public async Task<Response<Job>> Handle(GetJobByIdQuery query, CancellationToken cancellationToken)
            {
                var job = await _jobRepository.GetByIdAsync(query.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                return new Response<Job>(job);
            }
        }
    }
}
