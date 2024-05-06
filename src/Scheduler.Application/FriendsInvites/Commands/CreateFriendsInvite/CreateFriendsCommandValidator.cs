using FluentValidation;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public class CreateFriendsCommandValidator : AbstractValidator<CreateFriendsInviteCommand>
{
    public CreateFriendsCommandValidator()
    {
        RuleFor(x => x.SenderId).NotEqual(default(Guid));
        RuleFor(x => x.AddressieId).NotEqual(default(Guid));
        RuleFor(x => x.Message).MaximumLength(500);
    }
}