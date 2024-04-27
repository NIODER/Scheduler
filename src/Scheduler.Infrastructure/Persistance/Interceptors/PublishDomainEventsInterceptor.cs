using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scheduler.Domain.Common;

namespace Scheduler.Infrastructure.Persistance.Interceptors;

public class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    private readonly IPublisher _publisher = publisher;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        await PublishDomainEventsAsync(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext? dbContext)
    {
        if (dbContext is null)
        {
            return;
        }
        var eventsEntities = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Where(entry => entry.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();
        var events = eventsEntities
            .SelectMany(entry => entry.DomainEvents)
            .ToList();
        eventsEntities.ForEach(e => e.ClearDomainEvents());
        foreach (var e in events)
        {
            await _publisher.Publish(e);
        }
    }
}