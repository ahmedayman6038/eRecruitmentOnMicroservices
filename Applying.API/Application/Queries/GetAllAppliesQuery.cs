using Applying.API.Application.Interfaces;
using Applying.API.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Applying.API.Application.Queries
{
    public class GetAllAppliesQuery : IRequest<PagedResponse<IEnumerable<GetAllAppliesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int StatusId { get; set; }
    }

    public class GetAllAppliesQueryHandler : IRequestHandler<GetAllAppliesQuery, PagedResponse<IEnumerable<GetAllAppliesViewModel>>>
    {
        private readonly IApplyRepository _applyRepository;
        private readonly IMapper _mapper;
        public GetAllAppliesQueryHandler(IApplyRepository applyRepository, IMapper mapper)
        {
            _applyRepository = applyRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllAppliesViewModel>>> Handle(GetAllAppliesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllAppliesParameter>(request);
            var apply = await _applyRepository.GetPagedReponseWithStatusAsync(validFilter.PageNumber, validFilter.PageSize, validFilter.StatusId);
            var applyViewModel = _mapper.Map<IEnumerable<GetAllAppliesViewModel>>(apply);
            return new PagedResponse<IEnumerable<GetAllAppliesViewModel>>(applyViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }

}
