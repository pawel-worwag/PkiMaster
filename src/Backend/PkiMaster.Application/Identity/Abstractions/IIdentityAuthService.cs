using PkiMaster.Application.Identity.Current;
using PkiMaster.Application.Identity.Login;
using PkiMaster.Application.Identity.Register;

namespace PkiMaster.Application.Identity.Abstractions;

public interface IIdentityAuthService
{
    Task<AuthResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken);
    Task<AuthResult> LoginAsync(LoginCommand command, CancellationToken cancellationToken);
    Task LogoutAsync(CancellationToken cancellationToken);
    Task<CurrentUser?> GetCurrentUserAsync(CancellationToken cancellationToken);
}
