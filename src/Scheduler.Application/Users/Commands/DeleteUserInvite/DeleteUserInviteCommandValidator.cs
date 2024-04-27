using FluentValidation;

namespace Scheduler.Application.Users.Commands.DeleteUserInvite;

public class DeleteUserInviteCommandValidator : AbstractValidator<DeleteUserInviteCommand>
{
    public DeleteUserInviteCommandValidator()
    {
        RuleFor(x => x.SenderId).NotEqual(default(Guid));
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
    }
}