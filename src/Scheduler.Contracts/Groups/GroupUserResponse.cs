namespace Scheduler.Contracts.Groups;

public record GroupUserResponse(
    Guid UserId,
    int Permissions
);