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

namespace Applying.API.Application.Queries
{
    public class GetApplyByIdQuery : IRequest<Response<Apply>>
    {
        public int Id { get; set; }
        public class GetApplyByIdQueryHandler : IRequestHandler<GetApplyByIdQuery, Response<Apply>>
        {
            private readonly IApplyRepository _applyRepository;
            public GetApplyByIdQueryHandler(IApplyRepository applyRepository)
            {
                _applyRepository = applyRepository;
            }
            public async Task<Response<Apply>> Handle(GetApplyByIdQuery query, CancellationToken cancellationToken)
            {
                var apply = await _applyRepository.GetApplyByIdAsync(query.Id);
                if (apply == null) throw new ApiException($"Apply Not Found.");
                return new Response<Apply>(apply);
            }
        }
    }
}
