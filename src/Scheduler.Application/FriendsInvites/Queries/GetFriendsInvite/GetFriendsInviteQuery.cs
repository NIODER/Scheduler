using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;

namespace Scheduler.Application.FriendsInvites.Queries.GetFriendsInvite;

public record GetFriendsInviteQuery(
    Guid InviteId,
    Guid UserId
) : IRequest<ICommandResult<FriendsInviteResult>>;