using FluentValidation;

namespace Scheduler.Application.Groups.Queries.GetGroupUser;

public class GetGroupUserByGroupAndUserIdQueryValidator : AbstractValidator<GetGroupUserByGroupAndUserIdQuery>
{
    public GetGroupUserByGroupAndUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotNull().NotEmpty().NotEqual(default(Guid));
    }
}