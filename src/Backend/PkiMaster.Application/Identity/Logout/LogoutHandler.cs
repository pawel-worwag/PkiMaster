using PkiMaster.Application.Identity.Abstractions;

namespace PkiMaster.Application.Identity.Logout;

public sealed class LogoutHandler(IIdentityAuthService authService)
{
    public Task HandleAsync(CancellationToken cancellationToken) =>
        authService.LogoutAsync(cancellationToken);
}
