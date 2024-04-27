using FluentValidation;

namespace Scheduler.Application.Users.Queries.GetUserInvite;

public class GetUserInviteQueryValidator : AbstractValidator<GetUserInviteQuery>
{
    public GetUserInviteQueryValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}