using Jobs.API.Application.Exceptions;
using Jobs.API.Application.Interfaces;
using Jobs.API.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobs.API.Application.Commands
{
    public class DeleteJobByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteJobByIdCommandHandler : IRequestHandler<DeleteJobByIdCommand, Response<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteJobByIdCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(DeleteJobByIdCommand command, CancellationToken cancellationToken)
            {
                var job = await _unitOfWork.Jobs.GetByIdAsync(command.Id);
                if (job == null) throw new ApiException($"Job Not Found.");
                _unitOfWork.Jobs.Delete(job);
                await _unitOfWork.CommitAsync();
                return new Response<int>(job.Id);
            }
        }
    }
}
