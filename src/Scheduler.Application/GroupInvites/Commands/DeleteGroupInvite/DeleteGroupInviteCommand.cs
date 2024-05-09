using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.GroupInvites.Common;

namespace Scheduler.Application.GroupInvites.Commands.DeleteGroupInvite;

public record DeleteGroupInviteCommand(
    Guid UserId,
    Guid GroupId,
    Guid InviteId
) : IRequest<AccessResultWrapper<GroupInviteResult>>;
