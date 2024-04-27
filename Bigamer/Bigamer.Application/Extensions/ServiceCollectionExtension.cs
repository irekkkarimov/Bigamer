using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bigamer.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
 
        return services;
    }
}