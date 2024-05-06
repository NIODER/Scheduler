namespace Scheduler.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,
    string Description,
    string Token
);