using Scheduler.Domain.Common;

namespace Scheduler.Domain.GroupAggregate.ValueObjects;

public sealed class GroupId : ValueObject
{
    public Guid Id { get; init; }

    private GroupId(Guid id)
    {
        Id = id;
    }

    public static GroupId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static GroupId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}