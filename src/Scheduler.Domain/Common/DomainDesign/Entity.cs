namespace Scheduler.Domain.Common.DomainDesign;

public abstract class Entity<TId> : IHasDomainEvents where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
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

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}