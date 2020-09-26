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
    public class GetAllJobsQuery : IRequest<PagedResponse<IEnumerable<GetAllJobsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, PagedResponse<IEnumerable<GetAllJobsViewModel>>>
    {
        private readonly IJobRepositoryAsync _jobRepository;
        private readonly IMapper _mapper;
        public GetAllJobsQueryHandler(IJobRepositoryAsync jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllJobsViewModel>>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllJobsParameter>(request);
            var job = await _jobRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var jobViewModel = _mapper.Map<IEnumerable<GetAllJobsViewModel>>(job);
            return new PagedResponse<IEnumerable<GetAllJobsViewModel>>(jobViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
