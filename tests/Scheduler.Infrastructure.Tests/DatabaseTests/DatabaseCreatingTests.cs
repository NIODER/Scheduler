using Microsoft.EntityFrameworkCore;
using Scheduler.Infrastructure.Persistance;
using Scheduler.Infrastructure.Persistance.Interceptors;

namespace Scheduler.Infrastructure.Tests.DatabaseTests;

public class DatabaseCreatingTests
{
    private readonly SchedulerDbContext context;

    //public DatabaseCreatingTests()
    //{
    //    context = new SchedulerDbContext(
    //        new DbContextOptionsBuilder<SchedulerDbContext>()
    //        .UseNpgsql("Username=postgres;Host=localhost;Port=5432;Password=123123")
    //        .Options,
    //        new PublishDomainEventsInterceptor(null)
    //    );
    //}

    [Fact]
    public void CreateDatabase()
    {
        context.Database.EnsureCreated();
    }
}