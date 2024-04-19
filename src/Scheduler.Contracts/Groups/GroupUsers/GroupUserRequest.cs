namespace Scheduler.Contracts.Groups.GroupUsers;

public record GroupUserRequest(
    Guid GroupId,
    Guid UserId,
    int Permissions
);