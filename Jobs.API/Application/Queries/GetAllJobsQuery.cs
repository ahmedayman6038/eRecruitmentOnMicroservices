using AutoMapper;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Queries
{
    public class GetAllJobsQuery : IRequest<PagedResponse<IEnumerable<JobViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CityId { get; set; }
    }

    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, PagedResponse<IEnumerable<JobViewModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllJobsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<JobViewModel>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllJobsParameter>(request);
            var job = await _unitOfWork.Jobs.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, validFilter.CityId);
            var jobViewModel = _mapper.Map<IEnumerable<JobViewModel>>(job);
            return new PagedResponse<IEnumerable<JobViewModel>>(jobViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
