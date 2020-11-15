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

namespace Jobs.API.Application.Queries
{
    public class GetJobByIdQuery : IRequest<Response<Job>>
    {
        public int Id { get; set; }
        public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Response<Job>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetJobByIdQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<Job>> Handle(GetJobByIdQuery query, CancellationToken cancellationToken)
            {
                var job = await _unitOfWork.Jobs.GetByIdAsync(query.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                return new Response<Job>(job);
            }
        }
    }
}
