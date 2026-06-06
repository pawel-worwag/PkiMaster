using PkiMaster.Application.Identity.Abstractions;

namespace PkiMaster.Application.Identity.Login;

public sealed class LoginHandler(IIdentityAuthService authService)
{
    public Task<AuthResult> HandleAsync(LoginCommand command, CancellationToken cancellationToken) =>
        authService.LoginAsync(command, cancellationToken);
}
