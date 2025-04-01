using System.Reflection;
using AnimalTrack.Services.Pipelines;
using FluentValidation;
using MediatR;
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

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}