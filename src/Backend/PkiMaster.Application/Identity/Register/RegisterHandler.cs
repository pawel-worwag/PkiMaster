using PkiMaster.Application.Identity.Abstractions;

namespace PkiMaster.Application.Identity.Register;

public sealed class RegisterHandler(IIdentityAuthService authService)
{
    public Task<AuthResult> HandleAsync(RegisterCommand command, CancellationToken cancellationToken) =>
        authService.RegisterAsync(command, cancellationToken);
}
