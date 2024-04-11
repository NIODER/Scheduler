using Scheduler.Domain.Common;

namespace Scheduler.Domain.GroupInviteAggregate.ValueObjects;

public sealed class GroupInviteId : ValueObject
{
    public Guid Id { get; init; }

    private GroupInviteId(Guid id)
    {
        Id = id;
    }

    public static GroupInviteId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static GroupInviteId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}