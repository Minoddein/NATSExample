using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommandsAndQueries()
            .AddValidatorsFromAssemblies([typeof(Inject).Assembly])
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Inject).Assembly));

        return services;
    }

    private static IServiceCollection AddCommandsAndQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes =>
                classes.AssignableToAny([typeof(IRequestHandler<,>), typeof(IRequestHandler<>)]))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        return services;
    }
}