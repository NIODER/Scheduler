using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.GroupInvites.Common;

namespace Scheduler.Application.GroupInvites.Commands.CreateGroupInvite;

public record CreateGroupInviteCommand(
    Guid GroupId,
    Guid CreatorId,
    DateTime? Expires,
    uint? Usages,
    long Permissions,
    string Message
) : IRequest<AccessResultWrapper<GroupInviteResult>>;
