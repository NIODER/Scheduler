namespace Scheduler.Contracts.Groups.GroupUsers;

public record UpdateGroupUserRequest(
    Guid UserId,
    int Permissions
);