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
            private readonly IUnitOfWork _unitOfWork;
            public DeleteApplyByIdCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(DeleteApplyByIdCommand command, CancellationToken cancellationToken)
            {
                var apply = await _unitOfWork.Applies.GetApplyByIdAsync(command.Id);
                if (apply == null) throw new ApiException($"Apply Not Found.");
                _unitOfWork.Applies.Delete(apply);
                await _unitOfWork.CommitAsync();
                return new Response<int>(apply.Id);
            }
        }
    }
}
