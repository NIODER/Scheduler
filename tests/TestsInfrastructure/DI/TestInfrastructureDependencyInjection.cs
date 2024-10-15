using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Application.Common.Interfaces.Persistance;
using Scheduler.Infrastructure.Persistance;
using Scheduler.Infrastructure.Persistance.Repositories;

namespace TestsInfrastructure.DI;

internal static class TestInfrastructureDependencyInjection
{
    public static IServiceCollection AddTestInfrastructure(this IServiceCollection services)
    {
        services.AddTestRepositories();
        return services;
    }

    private static IServiceCollection AddTestRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IProblemsRepository, ProblemsRepository>();
        services.AddScoped<IGroupsRepository, GroupsRepository>();
        services.AddScoped<IFinancialPlansRepository, FinancialPlansRepository>();
        services.AddDbContext<SchedulerDbContext>(options
            => options.UseSqlite(new SqliteConnection(Resource.TestsDatabaseConnectionString)));
        return services;
    }
}
