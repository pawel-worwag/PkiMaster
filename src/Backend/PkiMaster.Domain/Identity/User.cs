namespace PkiMaster.Domain.Identity;

public sealed record User(
    Guid Id,
    string UserName,
    string? Email,
    bool EmailConfirmed,
    int AccessFailedCount,
    bool LockoutEnabled,
    DateTimeOffset? LockoutEnd,
    IReadOnlyList<string> Roles);
