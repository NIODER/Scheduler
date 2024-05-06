using MediatR;
using Scheduler.Application.FriendsInvites.Common;

namespace Scheduler.Application.FriendsInvites.Commands.AcceptFriendsInvite;

public record AcceptFriendsInviteCommand(
    Guid InviteId,
    Guid ExecutorId
) : IRequest<FriendsInviteResult>;
