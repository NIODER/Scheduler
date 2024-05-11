using FluentValidation;

namespace Scheduler.Application.Problems.Queries.GetGroupProblems;

internal class GetGroupProblemsCommandValidator : AbstractValidator<GetGroupProblemsCommand>
{
    public GetGroupProblemsCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}
