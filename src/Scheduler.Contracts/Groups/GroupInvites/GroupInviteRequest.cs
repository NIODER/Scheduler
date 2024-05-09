namespace Scheduler.Contracts.Groups.GroupInvites;

public record GroupInviteRequest(
    uint Usages,
    DateTime Expire,
    long Permissions,
    string Message
);
