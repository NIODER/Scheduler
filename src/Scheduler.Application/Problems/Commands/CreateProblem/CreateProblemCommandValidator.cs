using FluentValidation;
using Scheduler.Domain.ProblemAggregate;

namespace Scheduler.Application.Problems.Commands.CreateProblem;

internal class CreateProblemCommandValidator : AbstractValidator<CreateProblemCommand>
{
    public CreateProblemCommandValidator()
    {
        RuleFor(x => x.CreatorId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Status).Must(x => Enum.IsDefined(typeof(ProblemStatus), x)).When(x => x is not null);
        RuleFor(x => x.Deadline).NotNull();
    }
}
