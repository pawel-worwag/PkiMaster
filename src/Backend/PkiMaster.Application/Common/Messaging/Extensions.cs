using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PkiMaster.Application.Common.Messaging;

public static class Extensions
{
    public static IServiceCollection RegisterHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        Type[] handlerInterfaceTypes = [typeof(IHandler<,>),typeof(IHandler<>)];
        
        var registrations = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .Select(type => new
            {
                ImplementationType = type.AsType(),
                ServiceTypes = type.ImplementedInterfaces
                    .Where(@interface => @interface.IsGenericType &&
                                         handlerInterfaceTypes.Contains(@interface.GetGenericTypeDefinition()))
                    .ToArray()
            })
            .Where(registration => registration.ServiceTypes.Length > 0);
        
        foreach (var registration in registrations)
        {
            foreach (var serviceType in registration.ServiceTypes)
            {
                services.AddScoped(serviceType, registration.ImplementationType);
            }
        }
        
        return services;
    }
}