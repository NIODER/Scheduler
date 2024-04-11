using Scheduler.Domain.Common;

namespace Scheduler.Domain.UserAggregate.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Id { get; init; }

    private UserId(Guid id)
    {
        Id = id;
    }

    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static UserId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}