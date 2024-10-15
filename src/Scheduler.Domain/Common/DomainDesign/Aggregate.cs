namespace Scheduler.Domain.Common.DomainDesign;

public abstract class Aggregate<TId> : Entity<TId> where TId : notnull
{
    protected Aggregate(TId id) : base(id)
    {
    }

    protected Aggregate()
    {
    }
}