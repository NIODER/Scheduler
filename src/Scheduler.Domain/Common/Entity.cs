namespace Scheduler.Domain.Common;

public abstract class Entity<TId> where TId : notnull
{
    public TId Id { get; private set; } = default!;

    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public override bool Equals(object? obj) => obj is Entity<TId> entity && Id.Equals(entity);

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity<TId> left, Entity<TId> right) => left.Equals(right);
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => left.Equals(right);
}