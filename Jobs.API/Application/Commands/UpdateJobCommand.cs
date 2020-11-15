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
    public class UpdateJobCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public int CityId { get; set; }

        public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, Response<int>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public UpdateJobCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(UpdateJobCommand command, CancellationToken cancellationToken)
            {
                var job = await _unitOfWork.Jobs.GetByIdAsync(command.Id);

                if (job == null)
                {
                    throw new ApiException($"Job Not Found.");
                }
                else
                {
                    job.Name = command.Name;
                    job.Description = command.Description;
                    job.Requirements = command.Requirements;
                    job.CityId = command.CityId;
                    _unitOfWork.Jobs.Update(job);
                    await _unitOfWork.CommitAsync();
                    return new Response<int>(job.Id);
                }
            }
        }
    }
}
