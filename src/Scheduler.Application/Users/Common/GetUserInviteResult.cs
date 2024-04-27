using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Application.Users.Common;

public record UserInviteResult(
    FriendsInviteId InviteId,
    UserId SenderId,
    UserId AddressieId,
    string Message
);