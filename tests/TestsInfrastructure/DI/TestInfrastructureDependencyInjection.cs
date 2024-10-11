using Microsoft.Extensions.DependencyInjection;
using Scheduler.Application.Common.Interfaces.Persistance;
using TestsInfrastructure.Repositories;

namespace TestsInfrastructure.DI;

internal static class TestInfrastructureDependencyInjection
{
    public static IServiceCollection AddTestInfrastructure(this IServiceCollection services)
    {
        services.AddTestInfrastructure();
        services.AddTestRepositories();
        return services;
    }

    private static IServiceCollection AddTestRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersInMemoryRepository>();
        services.AddScoped<IProblemsRepository, ProblemsInMemoryRepository>();
        services.AddScoped<IGroupsRepository, GroupsInMemoryRepository>();
        services.AddScoped<IFinancialPlansRepository, FInancialPlansInMemoryRepository>();
        return services;
    }
}
