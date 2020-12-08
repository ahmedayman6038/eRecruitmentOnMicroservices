using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Entities;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Jobs.API.Application.Queries
{
    public class GetJobByIdQuery : IRequest<Response<JobViewModel>>
    {
        public int Id { get; set; }
        public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Response<JobViewModel>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetJobByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }
            public async Task<Response<JobViewModel>> Handle(GetJobByIdQuery query, CancellationToken cancellationToken)
            {
                var job = await _unitOfWork.Jobs.GetByIdAsync(query.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                var jobViewModel = _mapper.Map<JobViewModel>(job);
                return new Response<JobViewModel>(jobViewModel);
            }
        }
    }
}
