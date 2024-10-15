using Microsoft.Extensions.DependencyInjection;
using Scheduler.Api.Common.Mapping;
using Scheduler.Application;

namespace TestsInfrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddTestMocks(this IServiceCollection services)
    {
        services.AddApplication();
        services.AddMappings();
        services.AddTestInfrastructure();
        return services;
    }
}
