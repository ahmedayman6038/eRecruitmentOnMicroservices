using FluentValidation;
using Jobs.API.Application.Commands;
using Jobs.API.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobs.API.Application.Validations
{
    public class CreateJobCommandValidator : AbstractValidator<CreateJobCommand>
    {
        private readonly IJobRepository jobRepository;

        public CreateJobCommandValidator(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.CityId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
