using FluentValidation;

namespace Scheduler.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
    }
}