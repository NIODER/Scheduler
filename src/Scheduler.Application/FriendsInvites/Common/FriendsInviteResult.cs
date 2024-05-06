using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.FriendsInvites.Common;

public record FriendsInviteResult(
    FriendsInviteId InviteId,
    UserId SenderId,
    UserId AddressieId,
    string Message
);