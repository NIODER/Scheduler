using FluentValidation;

namespace Scheduler.Application.Groups.Commands.UpdateGroupUser;

public class UpdateGroupUserCommandValidator : AbstractValidator<UpdateGroupUserCommand>
{
    public UpdateGroupUserCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
    }
}