using FluentValidation;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Users.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandValidator : AbstractValidator<UpdateUserSettingsCommand>
{
    public UpdateUserSettingsCommandValidator()
    {
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.Permissions).Must(i => Enum.IsDefined(typeof(UserPrivateSettings), i));
    }
}