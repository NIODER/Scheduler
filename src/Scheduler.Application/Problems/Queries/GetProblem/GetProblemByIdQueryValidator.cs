using FluentValidation;

namespace Scheduler.Application.Problems.Queries.GetProblem;

internal class GetProblemByIdQueryValidator : AbstractValidator<GetProblemByIdQuery>
{
    public GetProblemByIdQueryValidator()
    {
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
        RuleFor(x => x.TaskId).NotEqual(default(Guid));
    }
}
