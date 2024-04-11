using Scheduler.Domain.Common;

namespace Scheduler.Domain.TaskAggregate.ValueObjects;

public sealed class TaskId : ValueObject
{
    public Guid Id { get; init; }

    private TaskId(Guid id)
    {
        Id = id;
    }

    public static TaskId CreateUnique()
    {
        return new(Guid.NewGuid());

    }
    public static TaskId Create(Guid id)
    {
        return new(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}