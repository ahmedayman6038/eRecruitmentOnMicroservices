using Applying.API.Application.Exceptions;
using Applying.API.Application.Interfaces;
using Applying.API.Application.Entities;
using Applying.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Applying.API.Application.Queries
{
    public class GetApplyByIdQuery : IRequest<Response<ApplyViewModel>>
    {
        public int Id { get; set; }
        public class GetApplyByIdQueryHandler : IRequestHandler<GetApplyByIdQuery, Response<ApplyViewModel>>
        {
            private readonly IApplyRepository _applyRepository;
            private readonly IMapper _mapper;

            public GetApplyByIdQueryHandler(IApplyRepository applyRepository, IMapper mapper)
            {
                _applyRepository = applyRepository;
                _mapper = mapper;
            }
            public async Task<Response<ApplyViewModel>> Handle(GetApplyByIdQuery query, CancellationToken cancellationToken)
            {
                var apply = await _applyRepository.GetApplyByIdAsync(query.Id);
                if (apply == null) throw new ApiException($"Apply Not Found.");
                var applyViewModel = _mapper.Map<ApplyViewModel>(apply);
                return new Response<ApplyViewModel>(applyViewModel);
            }
        }
    }
}
