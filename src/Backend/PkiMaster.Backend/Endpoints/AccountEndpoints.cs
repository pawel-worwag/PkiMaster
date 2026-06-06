using Microsoft.AspNetCore.Mvc;
using PkiMaster.Application.Identity.Login;
using PkiMaster.Application.Identity.Logout;

namespace PkiMaster.Backend.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/account").WithTags("Account");

        group.MapPost("/login", LoginAsync).AllowAnonymous();
        group.MapPost("/logout", LogoutAsync).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> LoginAsync(
        [FromForm] string email,
        [FromForm] string password,
        [FromForm] bool? rememberMe,
        LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new LoginCommand(email, password, rememberMe ?? false), cancellationToken);
        if (result.Succeeded)
        {
            return Results.LocalRedirect("/");
        }

        return Results.LocalRedirect("/login?error=invalid-credentials");
    }

    private static async Task<IResult> LogoutAsync(
        LogoutHandler handler,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(cancellationToken);
        return Results.LocalRedirect("/login");
    }
}
