using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Queries.GetUserInvite;

public record GetUserInviteQuery(
    Guid InviteId,
    Guid UserId
) : IRequest<UserInviteResult>;