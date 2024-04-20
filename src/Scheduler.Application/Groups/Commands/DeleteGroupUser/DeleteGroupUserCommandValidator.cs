using FluentValidation;

namespace Scheduler.Application.Groups.Commands.DeleteGroupUser;

public class DeleteGroupUserCommandValidator : AbstractValidator<DeleteGroupUserCommand>
{
    public DeleteGroupUserCommandValidator()
    {
        RuleFor(x => x.ExecutorId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.GroupId).NotNull().NotEmpty().NotEqual(default(Guid));
    }
}