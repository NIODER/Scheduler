using FluentValidation;

namespace Scheduler.Application.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotNull().NotEqual(default(Guid));
    }
}