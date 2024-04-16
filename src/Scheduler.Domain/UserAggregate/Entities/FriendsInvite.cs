using Scheduler.Domain.Common;
using Scheduler.Domain.UserAggregate.ValueObjects;

namespace Scheduler.Domain.UserAggregate.Entities;

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
}