namespace Scheduler.Contracts.Groups.GroupInvites;

public record GroupInviteRequest(
    int Uses,
    DateTime Expire,
    long Permissions,
    string Message
);
