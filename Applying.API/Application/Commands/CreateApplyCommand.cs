using Applying.API.Application.Interfaces;
using Applying.API.Application.Entities;
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
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            public CreateApplyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateApplyCommand request, CancellationToken cancellationToken)
            {
                var apply = _mapper.Map<Apply>(request);
                await _unitOfWork.Applies.AddAsync(apply);
                await _unitOfWork.CommitAsync();
                return new Response<int>(apply.Id);
            }
        }
    }
}
