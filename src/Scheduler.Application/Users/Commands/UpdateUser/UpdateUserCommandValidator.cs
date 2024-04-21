using FluentValidation;

namespace Scheduler.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.Name).MaximumLength(120);
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}