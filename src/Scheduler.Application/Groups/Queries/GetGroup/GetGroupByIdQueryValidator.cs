using FluentValidation;

namespace Scheduler.Application.Groups.Queries.GetGroup;

public class GetGroupByIdQueryValidator : AbstractValidator<GetGroupByIdQuery>
{
    public GetGroupByIdQueryValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
    }
}