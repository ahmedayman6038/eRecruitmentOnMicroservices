using Applying.API.Application.Commands;
using Applying.API.Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Applying.API.Application.Validations
{
    public class CreateApplyCommandValidator : AbstractValidator<CreateApplyCommand>
    {
        private readonly IApplyRepository applyRepository;

        public CreateApplyCommandValidator(IApplyRepository applyRepository)
        {
            this.applyRepository = applyRepository;

            RuleFor(p => p.JobId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync((cmd, jobId, token) => IsNotApplied(jobId, cmd.UserId, token)).WithMessage("{PropertyName} already applied to this user.");

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("{PropertyName} is required.");

        }

        private async Task<bool> IsNotApplied(int jobId, string userId, CancellationToken cancellationToken)
        {
            return !await applyRepository.IsAppliedAsync(jobId, userId);
        }
    }
}
