using FluentValidation;

namespace Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;

public class DeleteFriendsInviteCommandValidator : AbstractValidator<DeleteFriendsInviteCommand>
{
    public DeleteFriendsInviteCommandValidator()
    {
        RuleFor(x => x.SenderId).NotEqual(default(Guid));
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
    }
}