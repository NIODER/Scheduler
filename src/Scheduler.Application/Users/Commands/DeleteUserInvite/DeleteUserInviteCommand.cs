using MediatR;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Commands.DeleteUserInvite;

public record DeleteUserInviteCommand(
    Guid SenderId,
    Guid InviteId
) : IRequest<UserInviteResult>;