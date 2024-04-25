using Microsoft.Extensions.DependencyInjection;

namespace Bigamer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}