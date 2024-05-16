using Bigamer.Application.Interfaces;
using Bigamer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bigamer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IConfirmationCodeGenerator, ConfirmationCodeGenerator>();
        
        services.AddScoped<IEmailSender, EmailSender>();
        
        return services;
    }
}