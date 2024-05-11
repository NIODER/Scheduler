using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Groups.Common;

namespace Scheduler.Application.Groups.Commands.CreateGroupInvite;

public record CreateGroupInviteCommand(
    Guid GroupId,
    Guid CreatorId,
    DateTime? Expires,
    uint? Usages,
    long Permissions,
    string Message
) : IRequest<ICommandResult<GroupInviteResult>>;
