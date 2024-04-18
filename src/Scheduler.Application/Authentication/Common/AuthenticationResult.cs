using Scheduler.Domain.UserAggregate;

namespace Scheduler.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);