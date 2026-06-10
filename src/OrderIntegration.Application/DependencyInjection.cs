using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderIntegration.Application.Common.Behaviors;
using System.Reflection;

namespace OrderIntegration.Application;
 
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        return services;
    }
}