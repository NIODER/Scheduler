using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string? Name,
    string? Description
) : IRequest<ICommandResult<UserResult>>;