using FluentValidation;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
        RuleFor(x => x.GroupName).MaximumLength(120);
    }
}