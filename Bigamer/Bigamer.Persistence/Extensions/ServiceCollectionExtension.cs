using Bigamer.Application.Interfaces;
using Bigamer.Domain.Entities;
using Bigamer.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bigamer.Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("POSTGRESQL_CONNECTION_STRING")));

        services.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}