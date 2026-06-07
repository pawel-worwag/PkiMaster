using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PkiMaster.Infrastructure.Identity;

public static class IdentitySeed
{
    public static async Task SeedSuperadminRoleAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var existingRole = await roleManager.FindByNameAsync("SuperAdmin");
        if (existingRole is not null)
        {
            return;
        }
        var role = new IdentityRole<Guid>("SuperAdmin");
        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Cannot create SuperAdmin role: {string.Join("; ", result.Errors.Select(x => x.Description))}");
        }

        result = await roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, "SuperAdmin"));
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Cannot add SuperAdmin role claim: {string.Join("; ", result.Errors.Select(x => x.Description))}");
        }
    }
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
        
        updateResult = await userManager.AddToRoleAsync(admin, "SuperAdmin");
        if (!updateResult.Succeeded)
        {
            throw new InvalidOperationException($"Cannot add Admin to SuperAdmin role: {string.Join("; ", updateResult.Errors.Select(x => x.Description))}");
        }
    }
}
