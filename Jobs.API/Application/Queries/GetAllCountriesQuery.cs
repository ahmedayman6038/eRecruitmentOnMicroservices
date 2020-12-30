using AutoMapper;
using Jobs.API.Application.Entities;
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
    public class GetAllCountriesQuery : IRequest<PagedResponse<IEnumerable<Country>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, PagedResponse<IEnumerable<Country>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCountriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<Country>>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCountriesParameter>(request);
            var countries = await _unitOfWork.Countries.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            return new PagedResponse<IEnumerable<Country>>(countries, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
