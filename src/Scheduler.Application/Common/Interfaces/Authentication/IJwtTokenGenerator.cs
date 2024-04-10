namespace Scheduler.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    // TODO add user based generation
    string GenerateJwtToken();
}