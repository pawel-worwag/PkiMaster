using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PkiMaster.Application.Identity.Abstractions;
using PkiMaster.Domain.Identity;

namespace PkiMaster.Infrastructure.Identity;

internal sealed class IdentityUserRepository(UserManager<ApplicationUser> userManager) : IIdentityUserRepository
{
    public async Task<IReadOnlyList<User>> ListAsync(CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .OrderBy(user => user.NormalizedUserName)
            .ToListAsync(cancellationToken);

        var domainUsers = new List<User>(users.Count);
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            domainUsers.Add(IdentityUserMapper.ToDomain(user, roles.ToArray()));
        }

        return domainUsers;
    }

    public async Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var roles = await userManager.GetRolesAsync(user);
        return IdentityUserMapper.ToDomain(user, roles.ToArray());
    }
}
