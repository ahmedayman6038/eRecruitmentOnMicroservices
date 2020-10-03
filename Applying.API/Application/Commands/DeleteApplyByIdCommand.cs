using Applying.API.Application.Exceptions;
using Applying.API.Application.Interfaces;
using Applying.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Applying.API.Application.Commands
{
    public class DeleteApplyByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteApplyByIdCommandHandler : IRequestHandler<DeleteApplyByIdCommand, Response<int>>
        {
            private readonly IApplyRepository _applyRepository;
            public DeleteApplyByIdCommandHandler(IApplyRepository applyRepository)
            {
                _applyRepository = applyRepository;
            }
            public async Task<Response<int>> Handle(DeleteApplyByIdCommand command, CancellationToken cancellationToken)
            {
                var apply = await _applyRepository.GetApplyByIdAsync(command.Id);
                if (apply == null) throw new ApiException($"Apply Not Found.");
                await _applyRepository.DeleteAsync(apply);
                return new Response<int>(apply.Id);
            }
        }
    }
}
