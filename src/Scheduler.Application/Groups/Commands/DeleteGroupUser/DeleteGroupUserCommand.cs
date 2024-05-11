using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.DeleteGroupUser;

public record DeleteGroupUserCommand(
    Guid GroupId,
    Guid UserId,
    Guid ExecutorId
) : IRequest<ICommandResult<GroupUserResult>>;