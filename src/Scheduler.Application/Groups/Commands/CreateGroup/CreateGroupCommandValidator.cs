using FluentValidation;

namespace Scheduler.Application.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.ExecutorId).NotNull().NotEmpty().NotEqual(default(Guid));
        RuleFor(x => x.GroupName).NotNull().NotEmpty().MaximumLength(120);
    }
}