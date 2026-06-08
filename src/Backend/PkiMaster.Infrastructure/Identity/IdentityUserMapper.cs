using PkiMaster.Domain.Identity;

namespace PkiMaster.Infrastructure.Identity;

internal static class IdentityUserMapper
{
    public static User ToDomain(ApplicationUser user, IReadOnlyList<string> roles) =>
        new(
            user.Id,
            user.UserName ?? string.Empty,
            user.Email,
            user.EmailConfirmed,
            user.AccessFailedCount,
            user.LockoutEnabled,
            user.LockoutEnd,
            roles);
}
