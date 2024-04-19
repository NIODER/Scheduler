namespace Scheduler.Application.Users.Common;

public record UserResult(
    Guid UserId,
    string Name,
    string Email,
    string Description
);