using MediatR;
using Scheduler.Application.Common.Wrappers;
using Scheduler.Application.FriendsInvites.Common;

namespace Scheduler.Application.FriendsInvites.Commands.CreateFriendsInvite;

public record CreateFriendsInviteCommand(
    Guid SenderId,
    Guid AddressieId,
    string Message
) : IRequest<ICommandResult<FriendsInviteResult>>;