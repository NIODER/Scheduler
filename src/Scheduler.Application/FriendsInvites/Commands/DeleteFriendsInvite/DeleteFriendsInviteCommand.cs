using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;

namespace Scheduler.Application.FriendsInvites.Commands.DeleteFriendsInvite;

public record DeleteFriendsInviteCommand(
    Guid SenderId,
    Guid InviteId
) : IRequest<ICommandResult<FriendsInviteResult>>;