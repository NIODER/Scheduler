namespace Scheduler.Contracts.Users;

public record UpdateUserRequest(
    Guid UserId,
    string? Name,
    string? Description
);