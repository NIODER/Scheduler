using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Users.Common;

public record UserSettingsResult(
    UserId UserId,
    UserPrivateSettings Settings
);