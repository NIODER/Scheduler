using MediatR;
using Scheduler.Application.GroupInvites.Common;

namespace Scheduler.Application.GroupInvites.Commands.AcceptGroupInvite;

public record AcceptGroupInviteCommand(
    Guid GroupId,
    Guid UserId,
    Guid InviteId
) : IRequest<GroupInviteResult>;
