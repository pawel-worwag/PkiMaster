using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PkiMaster.Application.Identity.Abstractions;
using PkiMaster.Application.Identity.Current;
using PkiMaster.Application.Identity.Login;
using PkiMaster.Application.Identity.Register;

namespace PkiMaster.Infrastructure.Identity;

public sealed class IdentityAuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IHttpContextAccessor httpContextAccessor) : IIdentityAuthService
{
    public async Task<AuthResult> RegisterAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email
        };

        var createResult = await userManager.CreateAsync(user, command.Password);
        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors.Select(error => error.Description).ToArray();
            return new AuthResult(false, errors);
        }

        await userManager.AddClaimAsync(user, new Claim("sub", user.Id.ToString()));
        await signInManager.SignInAsync(user, isPersistent: false);
        return new AuthResult(true, []);
    }

    public async Task<AuthResult> LoginAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return new AuthResult(false, ["Invalid credentials."]);
        }

        var signInResult = await signInManager.PasswordSignInAsync(
            user,
            command.Password,
            command.RememberMe,
            lockoutOnFailure: true);

        if (signInResult.Succeeded)
        {
            return new AuthResult(true, []);
        }

        if (signInResult.IsLockedOut)
        {
            return new AuthResult(false, ["User is temporarily locked out due to failed login attempts."]);
        }

        return new AuthResult(false, ["Invalid credentials."]);
    }

    public Task LogoutAsync(CancellationToken cancellationToken) => signInManager.SignOutAsync();

    public async Task<CurrentUser?> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var principal = httpContextAccessor.HttpContext?.User;
        if (principal?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var user = await userManager.GetUserAsync(principal);
        if (user?.Email is null)
        {
            return null;
        }

        return new CurrentUser(user.Id, user.Email);
    }
}
