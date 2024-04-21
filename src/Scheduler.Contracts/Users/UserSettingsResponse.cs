namespace Scheduler.Contracts.Users;

public record UserSettingsResponse(
    Guid UserId,
    int Settings
);