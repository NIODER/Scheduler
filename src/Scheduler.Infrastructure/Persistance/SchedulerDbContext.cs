using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.FinancialPlanAggregate;
using Scheduler.Domain.GroupAggregate;
using Scheduler.Domain.ProblemAggregate;
using Scheduler.Domain.UserAggregate;

namespace Scheduler.Infrastructure.Persistance;

public sealed class SchedulerDbContext(DbContextOptions<SchedulerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; }
    public DbSet<Group> Groups { get; private set; }
    public DbSet<Problem> Problems { get; private set; }
    public DbSet<FinancialPlan> FinancialPlans { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}