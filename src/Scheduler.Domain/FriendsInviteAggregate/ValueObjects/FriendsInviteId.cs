using Scheduler.Domain.Common;

namespace Scheduler.Domain.FriendsInviteAggregate.ValueObjects;

public sealed class FriendsInviteId : ValueObject
{
    public Guid Id { get; init; }

    private FriendsInviteId(Guid id)
    {
        Id = id;
    }

    public static FriendsInviteId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static FriendsInviteId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}