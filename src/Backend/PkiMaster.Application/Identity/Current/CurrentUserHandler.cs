using PkiMaster.Application.Identity.Abstractions;

namespace PkiMaster.Application.Identity.Current;

public sealed class CurrentUserHandler(IIdentityAuthService authService)
{
    public Task<CurrentUser?> HandleAsync(CancellationToken cancellationToken) =>
        authService.GetCurrentUserAsync(cancellationToken);
}
