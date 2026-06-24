namespace PkiMaster.Dto.Identity.GetAllUsers;

public record User
{
    public required Guid Id { get; init; }
    public required string UserName { get; init; }
    public required string? Email { get; init; }
    public required bool EmailConfirmed { get; init; }
    public required int AccessFailedCount { get; init; }
    public required bool LockoutEnabled { get; init; }
    public DateTimeOffset? LockoutEnd { get; init; }
    public required IReadOnlyList<string> Roles { get; init; }
}