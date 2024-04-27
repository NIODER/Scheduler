using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Commands.AcceptUserInvite;

public record AcceptUserInviteCommand(
    Guid InviteId,
    Guid ExecutorId 
) : IRequest<UserInviteResult>;
