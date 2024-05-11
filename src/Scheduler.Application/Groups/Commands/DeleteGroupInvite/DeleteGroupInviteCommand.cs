using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.DeleteGroupInvite;

public record DeleteGroupInviteCommand(
    Guid UserId,
    Guid GroupId,
    Guid InviteId
) : IRequest<ICommandResult<GroupInviteResult>>;
