using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.UpdateGroupUser;

public record UpdateGroupUserCommand(
    Guid GroupId,
    Guid UserId,
    Guid ExecutorId,
    int Permissions
) : IRequest<ICommandResult<GroupUserResult>>;