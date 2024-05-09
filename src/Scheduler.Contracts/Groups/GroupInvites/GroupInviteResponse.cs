namespace Scheduler.Contracts.Groups.GroupInvites;

public record GroupInviteResponse(
    Guid GroupId,
    Guid InviteId,
    Guid SenderId,
    long Permissions,
    string Message
);
