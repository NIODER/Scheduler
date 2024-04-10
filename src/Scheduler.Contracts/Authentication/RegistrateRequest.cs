namespace Scheduler.Contracts.Authentication;

public record RegistrateRequest(
    string Username,
    string Email,
    string Description,
    string Password,
    string PasswordConfirm
);