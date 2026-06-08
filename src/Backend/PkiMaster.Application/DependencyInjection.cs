using Microsoft.Extensions.DependencyInjection;
using PkiMaster.Application.Common.Messaging;
using PkiMaster.Application.Identity.Current;
using PkiMaster.Application.Identity.Login;
using PkiMaster.Application.Identity.Logout;
using PkiMaster.Application.Identity.Register;

namespace PkiMaster.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterHandler>();
        services.AddScoped<LoginHandler>();
        services.AddScoped<LogoutHandler>();
        services.AddScoped<CurrentUserHandler>();
        
        services.RegisterHandlersFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}
