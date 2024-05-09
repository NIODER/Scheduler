using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Common;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.UserAggregate;
using Scheduler.Domain.UserAggregate.Entities;
using Scheduler.Infrastructure.Persistance.Interceptors;

namespace Scheduler.Infrastructure.Persistance;

public sealed class SchedulerDbContext(DbContextOptions<SchedulerDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Group> Groups { get; private set; }
    public DbSet<Problem> Problems { get; private set; }
    public DbSet<FinancialPlan> FinancialPlans { get; private set; }
    public DbSet<FriendsInvite> FriendsInvites { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}