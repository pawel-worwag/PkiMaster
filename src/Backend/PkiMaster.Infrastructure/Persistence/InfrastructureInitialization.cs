using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PkiMaster.Infrastructure.Identity;

namespace PkiMaster.Infrastructure.Persistence;

public static class InfrastructureInitialization
{
    public static async Task InitialiseInfrastructureAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PkiMasterIdentityDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await IdentitySeed.SeedSuperadminRoleAsync(scope.ServiceProvider);
        await IdentitySeed.SeedAdminAsync(scope.ServiceProvider);
    }
}
