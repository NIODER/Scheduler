using FluentValidation;

namespace Scheduler.Application.Groups.Commands.UpdateGroupUser;

public class UpdateGroupUserCommandValidator : AbstractValidator<UpdateGroupUserCommand>
{
    public UpdateGroupUserCommandValidator()
    {
        RuleFor(x => x.GroupId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotNull().NotEmpty().NotEqual(default(Guid));
    }
}