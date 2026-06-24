using Microsoft.EntityFrameworkCore;
using PkiMaster.Application.Identity.Abstractions;
using PkiMaster.Domain.Identity;
using PkiMaster.Infrastructure.Persistence;

namespace PkiMaster.Infrastructure.Identity;

internal sealed class IdentityUserRepository(PkiMasterIdentityDbContext dbContext) : IIdentityUserRepository
{
    public async Task<IReadOnlyList<User>> ListAsync(CancellationToken cancellationToken)
    {
        var users = await dbContext.Users
            .AsNoTracking()
            .OrderBy(user => user.NormalizedUserName)
            .Select(user => IdentityUserMapper.ToDomain(user,(from userRole in dbContext.UserRoles
                join role in dbContext.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == user.Id
                select role.Name ?? string.Empty).ToArray()))
            .ToListAsync(cancellationToken);
        return users;
    }

    public async Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userWithRoles = await dbContext.Users
            .AsNoTracking()
            .Where(user => user.Id == id)
            .Select(user => IdentityUserMapper.ToDomain(user,(from userRole in dbContext.UserRoles
                join role in dbContext.Roles on userRole.RoleId equals role.Id
                where userRole.UserId == user.Id
                select role.Name ?? string.Empty).ToArray()))
            .FirstOrDefaultAsync(cancellationToken);
        return userWithRoles;
    }
}
