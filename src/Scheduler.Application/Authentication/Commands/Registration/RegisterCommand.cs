using MediatR;
using Scheduler.Application.Authentication.Common;
using Scheduler.Application.Common.Wrappers;

namespace Scheduler.Application.Authentication.Commands.Registration;

public record RegisterCommand(
    string Username,
    string Email,
    string Description,
    string Password
) : IRequest<ICommandResult<AuthenticationResult>>;