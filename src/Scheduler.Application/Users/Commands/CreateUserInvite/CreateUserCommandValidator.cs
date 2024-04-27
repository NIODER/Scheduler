using FluentValidation;

namespace Scheduler.Application.Users.Commands.CreateUserInvite;

public class CreateUserCommandValidator : AbstractValidator<CreateUserInviteCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.SenderId).NotEqual(default(Guid));
        RuleFor(x => x.AddressieId).NotEqual(default(Guid));
        RuleFor(x => x.Message).MaximumLength(500);
    }
}