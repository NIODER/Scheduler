using FluentValidation;

namespace Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;

public class AcceptFriendsInviteCommandValidator : AbstractValidator<AcceptFriendsInviteCommand>
{
    public AcceptFriendsInviteCommandValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
    }
}