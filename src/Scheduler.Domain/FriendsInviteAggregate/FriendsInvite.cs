using Scheduler.Domain.Common;
using Scheduler.Domain.FriendsInviteAggregate.Events;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.FriendsInviteAggregate;

public class FriendsInvite : Entity<FriendsInviteId>
{
    public UserId SenderId { get; private set; }
    public UserId AddressieId { get; private set; }
    public string Message { get; private set; }

    private FriendsInvite()
    {
        SenderId = default!;
        AddressieId = default!;
        Message = null!;
    }

    private FriendsInvite(
        FriendsInviteId id,
        UserId senderId,
        UserId addressieId,
        string message) : base(id)
    {
        SenderId = senderId;
        AddressieId = addressieId;
        Message = message;
    }

    public static FriendsInvite Create(
        UserId senderId,
        UserId addressieId,
        string message
    ) => new(new FriendsInviteId(Guid.NewGuid()), senderId, addressieId, message);

    public static FriendsInvite CreateWithFriendsInviteCreatedEvent(
        UserId senderId,
        UserId addressieId,
        string message)
    {
        var invite = new FriendsInvite(new(Guid.NewGuid()), senderId, addressieId, message);
        invite.AddDomainEvent(new FriendsInviteCreatedEvent(invite));
        return invite;
    }

    public bool UserIsRelated(UserId userId) => userId == SenderId || userId == AddressieId;
}