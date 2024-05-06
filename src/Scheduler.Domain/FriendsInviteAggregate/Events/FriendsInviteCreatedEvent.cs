using Scheduler.Domain.Common;
using Scheduler.Domain.FriendsInviteAggregate;

namespace Scheduler.Application.FriendsInvites.Events
{
    public record FriendsInviteCreatedEvent(FriendsInvite FriendsInvite) : IDomainEvent;
}
