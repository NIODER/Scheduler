using MediatR;
using Scheduler.Application.Authentication.Common;

namespace Scheduler.Application.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<AuthenticationResult>;