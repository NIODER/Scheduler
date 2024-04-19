namespace Scheduler.Contracts.Users;

public record UserGeneralInformationResponse(
    Guid UserId,
    string Name,
    string Description
);