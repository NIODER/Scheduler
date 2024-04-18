using FluentValidation;

namespace Scheduler.Application.Authentication.Commands.Registration;

public class RegistrationCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Username).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Password).MinimumLength(8).NotEmpty();
    }
}