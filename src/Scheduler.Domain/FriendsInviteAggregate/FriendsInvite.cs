using Scheduler.Domain.Common;
using Scheduler.Domain.FriendsInviteAggregate.ValueObjects;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.FriendsInviteAggregate;

public class FriendsInvite : Aggregate<FriendsInviteId>
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
        FriendsInviteId friendsInviteId,
        UserId senderId,
        UserId addressieId,
        string message
    ) : base(friendsInviteId)
    {
        SenderId = senderId;
        AddressieId = addressieId;
        Message = message;
    }

    public static FriendsInvite Create(
        UserId senderId,
        UserId addressieId,
        string message
    ) => new(FriendsInviteId.CreateUnique(), senderId: senderId, addressieId: addressieId, message);
}