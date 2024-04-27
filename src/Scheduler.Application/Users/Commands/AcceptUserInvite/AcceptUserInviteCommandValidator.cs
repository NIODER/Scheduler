using FluentValidation;

namespace Scheduler.Application.Users.Commands.AcceptUserInvite;

public class AcceptUserInviteCommandValidator : AbstractValidator<AcceptUserInviteCommand>
{
    public AcceptUserInviteCommandValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
    }
}