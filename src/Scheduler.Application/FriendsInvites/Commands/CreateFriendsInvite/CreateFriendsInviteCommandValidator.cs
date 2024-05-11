using FluentValidation;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsInviteCommandValidator : AbstractValidator<CreateFriendsInviteCommand>
{
    public CreateFriendsInviteCommandValidator()
    {
        RuleFor(x => x.SenderId).NotEqual(default(Guid));
        RuleFor(x => x.AddressieId).NotEqual(default(Guid));
        RuleFor(x => x.Message).MaximumLength(500);
    }
}