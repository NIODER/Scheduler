using FluentValidation;

namespace Scheduler.Application.Users.Queries.GetUserSettings;

public class GetUserSettingsQueryValidator : AbstractValidator<GetUserSettingsQuery>
{
    public GetUserSettingsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEqual(default(Guid));
        RuleFor(x => x.ExecutorId).NotEqual(default(Guid));
    }
}
