using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalTrack.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServiceDependencies(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}