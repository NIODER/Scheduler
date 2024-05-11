using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.AcceptGroupInvite;

public record AcceptGroupInviteCommand(
    Guid GroupId,
    Guid UserId,
    Guid InviteId
) : IRequest<ICommandResult<GroupInviteResult>>;
