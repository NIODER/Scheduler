using Microsoft.Extensions.DependencyInjection;

namespace TestsInfrastructure.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddTestMocks(this IServiceCollection services)
    {
        services.AddTestInfrastructure();
        return services;
    }
}
