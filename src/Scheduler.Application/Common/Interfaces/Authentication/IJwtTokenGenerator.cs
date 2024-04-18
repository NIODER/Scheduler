using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(User user);
}