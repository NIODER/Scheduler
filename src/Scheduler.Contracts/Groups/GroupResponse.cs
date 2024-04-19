namespace Scheduler.Contracts.Groups;

public record GroupResponse(
    Guid GroupId,
    string GroupName,
    List<GroupUserResponse> Users
);