using Microsoft.Extensions.DependencyInjection;
using TestsInfrastructure.DI;

namespace TestsInfrastructure;

public static class DependenciesContainer
{
    public static IServiceCollection Services => new ServiceCollection().AddTestMocks();
}
