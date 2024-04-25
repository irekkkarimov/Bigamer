using Microsoft.Extensions.DependencyInjection;

namespace Bigamer.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}