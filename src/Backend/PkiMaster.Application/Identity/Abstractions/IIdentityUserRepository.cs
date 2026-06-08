using PkiMaster.Domain.Identity;

namespace PkiMaster.Application.Identity.Abstractions;

public interface IIdentityUserRepository
{
    Task<IReadOnlyList<User>> ListAsync(CancellationToken cancellationToken);
    Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
}
