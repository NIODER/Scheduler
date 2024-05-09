using FluentValidation;

namespace Scheduler.Application.GroupInvites.Commands.DeleteGroupInvite;

public class DeleteGroupInviteCommandValidator : AbstractValidator<DeleteGroupInviteCommand>
{
    public DeleteGroupInviteCommandValidator()
    {
        RuleFor(x => x.InviteId).NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
    }
}
