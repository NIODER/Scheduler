namespace Scheduler.Contracts.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Username,
    string Email,
    string Description,
    string Token
);