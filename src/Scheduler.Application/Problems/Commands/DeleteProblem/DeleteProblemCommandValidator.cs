using FluentValidation;

namespace Scheduler.Application.Problems.Commands.DeleteProblem;

internal class DeleteProblemCommandValidator : AbstractValidator<DeleteProblemCommand>
{
    public DeleteProblemCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.ProblemId).NotEqual(default(Guid));
    }
}
