using FluentValidation;

namespace Scheduler.Application.Groups.Commands.DeleteGroupUser;

public class DeleteGroupUserCommandValidator : AbstractValidator<DeleteGroupUserCommand>
{
    public DeleteGroupUserCommandValidator()
    {
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
    }
}