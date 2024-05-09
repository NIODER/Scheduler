using FluentValidation;

namespace Scheduler.Application.GroupInvites.Commands.AcceptGroupInvite;

public class AcceptGroupInviteCommandValidator : AbstractValidator<AcceptGroupInviteCommand>
{
    public AcceptGroupInviteCommandValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}
