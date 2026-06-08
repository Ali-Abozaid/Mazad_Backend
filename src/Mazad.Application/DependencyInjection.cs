using FluentValidation;
using Mazad.SharedKernel.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mazad.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMazadApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
