namespace Scheduler.Contracts.Groups.GroupUsers;

public record GroupUserResponse(
    Guid GroupId,
    Guid UserId,
    int Permissions
);