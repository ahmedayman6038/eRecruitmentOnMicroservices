using Applying.API.Application.Interfaces;
using Applying.API.Application.Models;
using Applying.API.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Applying.API.Application.Commands
{
    public class CreateApplyCommand : IRequest<Response<int>>
    {
        public string UserId { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public class CreateApplyCommandHandler : IRequestHandler<CreateApplyCommand, Response<int>>
        {
            private readonly IApplyRepository _applyRepository;
            private readonly IMapper _mapper;
            public CreateApplyCommandHandler(IApplyRepository applyRepository, IMapper mapper)
            {
                _applyRepository = applyRepository;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateApplyCommand request, CancellationToken cancellationToken)
            {
                var apply = _mapper.Map<Apply>(request);
                await _applyRepository.AddAsync(apply);
                return new Response<int>(apply.Id);
            }
        }
    }
}
