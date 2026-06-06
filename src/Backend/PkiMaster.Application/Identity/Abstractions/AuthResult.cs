namespace PkiMaster.Application.Identity.Abstractions;

public sealed record AuthResult(bool Succeeded, IReadOnlyList<string> Errors);
