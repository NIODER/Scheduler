using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Persistance;

namespace Scheduler.InfrastructureUnitTests.DatabaseTests;

public class DatabaseCreatingTests
{
    private readonly SchedulerDbContext context;

    public DatabaseCreatingTests()
    {
        context = new SchedulerDbContext(
            new DbContextOptionsBuilder<SchedulerDbContext>()
            .UseNpgsql("Username=postgres;Host=localhost;Port=5432;Password=123123")
            .Options
        );
    }

    [Fact]
    public void CreateDatabase()
    {
        context.Database.EnsureCreated();
    }
}