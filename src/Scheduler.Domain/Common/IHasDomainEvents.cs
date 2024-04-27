namespace Scheduler.Domain.Common;

public interface IHasDomainEvents
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}
