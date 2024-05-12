namespace Scheduler.Contracts.Authentication;

public record UserDataResponse(
    Guid Id,
    string Name,
    string Email,
    string Description,
    string Token
);
