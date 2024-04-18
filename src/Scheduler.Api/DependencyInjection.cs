using Scheduler.Api.Common.Mapping;

namespace Scheduler.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMappings();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}