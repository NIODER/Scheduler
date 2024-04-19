using FluentValidation;

namespace Scheduler.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}