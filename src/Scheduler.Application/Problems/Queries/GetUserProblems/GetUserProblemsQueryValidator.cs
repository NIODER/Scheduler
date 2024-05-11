using FluentValidation;

namespace Scheduler.Application.Problems.Queries.GetUserProblems;

internal class GetUserProblemsQueryValidator : AbstractValidator<GetUserProblemsQuery>
{
    public GetUserProblemsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}
