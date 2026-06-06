using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PkiMaster.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task SeedAdminAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var existingAdmin = await userManager.FindByNameAsync("Admin");
        if (existingAdmin is not null)
        {
            return;
        }

        var admin = new ApplicationUser
        {
            UserName = "Admin",
            Email = "admin@localhost",
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(admin);
        if (!createResult.Succeeded)
        {
            throw new InvalidOperationException($"Cannot create Admin user: {string.Join("; ", createResult.Errors.Select(x => x.Description))}");
        }

        var passwordHasher = services.GetRequiredService<IPasswordHasher<ApplicationUser>>();
        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin");

        var updateResult = await userManager.UpdateAsync(admin);
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException($"Cannot set Admin password: {string.Join("; ", updateResult.Errors.Select(x => x.Description))}");
        }
    }
}
