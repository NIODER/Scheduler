using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Wrappers;

namespace Scheduler.Application.Authentication.Commands.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<ICommandResult<AuthenticationResult>>;