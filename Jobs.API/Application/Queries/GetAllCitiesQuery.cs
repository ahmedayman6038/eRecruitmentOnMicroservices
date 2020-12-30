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
    public class GetAllCitiesQuery : IRequest<PagedResponse<IEnumerable<City>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CountryId { get; set; }
    }

    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, PagedResponse<IEnumerable<City>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<City>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCitiesParameter>(request);
            var cities = await _unitOfWork.Cities.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, validFilter.CountryId);
            return new PagedResponse<IEnumerable<City>>(cities, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
