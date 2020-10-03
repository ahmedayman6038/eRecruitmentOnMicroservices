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
    public class UpdateApplyCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        public int Status { get; set; }

        public class UpdateApplyCommandHandler : IRequestHandler<UpdateApplyCommand, Response<int>>
        {
            private readonly IApplyRepository _applyRepository;
            public UpdateApplyCommandHandler(IApplyRepository applyRepository)
            {
                _applyRepository = applyRepository;
            }

            public async Task<Response<int>> Handle(UpdateApplyCommand command, CancellationToken cancellationToken)
            {
                var apply = await _applyRepository.GetApplyByIdAsync(command.Id);

                if (apply == null)
                {
                    throw new ApiException($"Apply Not Found.");
                }
                else
                {
                    apply.UserId = command.UserId;
                    apply.JobId = command.JobId;
                    apply.Status = command.Status;
                    await _applyRepository.UpdateAsync(apply);
                    return new Response<int>(apply.Id);
                }
            }
        }
    }
}
