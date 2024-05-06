using Scheduler.Domain.Common;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate.DomainEvents;

public record UserAcceptedFriendInviteEvent(
    UserId SenderId,
    UserId AddressieId
) : IDomainEvent;