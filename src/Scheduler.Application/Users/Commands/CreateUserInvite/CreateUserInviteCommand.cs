using MediatR;
using Scheduler.Application.Users.Common;

namespace Scheduler.Application.Users.Commands.CreateUserInvite;

public record CreateUserInviteCommand(
    Guid SenderId,
    Guid AddressieId,
    string Message
) : IRequest<UserInviteResult>;