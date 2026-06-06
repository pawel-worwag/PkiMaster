using System.ComponentModel.DataAnnotations;

namespace PkiMaster.Application.Identity.Login;

public sealed record LoginCommand(
    [property: Required, EmailAddress] string Email,
    [property: Required] string Password,
    bool RememberMe = false);
