using System.ComponentModel.DataAnnotations;

namespace PkiMaster.Application.Identity.Register;

public sealed record RegisterCommand(
    [property: Required, EmailAddress] string Email,
    [property: Required, MinLength(12)] string Password);
