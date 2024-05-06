using FluentValidation;

namespace Scheduler.Application.FriendsInvites.Queries.GetFriendsInvite;

public class GetFriendsInviteQueryValidator : AbstractValidator<GetFriendsInviteQuery>
{
    public GetFriendsInviteQueryValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}