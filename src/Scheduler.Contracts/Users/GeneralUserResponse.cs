namespace Scheduler.Contracts.Users;

public record GeneralUserResponse(
    Guid UserId,
    string Name,
    string Email,
    string Description
);