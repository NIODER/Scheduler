namespace Scheduler.Domain.Common.DomainDesign;

public interface IHasDomainEvents
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    public void ClearDomainEvents();
}
