namespace PCI.Shared.Dtos;

public record UserDto
{
    public string Id { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string UserName { get; init; }

    public bool EmailConfirmed { get; init; }

    public bool PhoneNumberConfirmed { get; init; }

    public bool TwoFactorEnabled { get; init; }

    public int AccessFailedCount { get; init; }

    public bool LockoutEnabled { get; init; }

    public bool IsDeleted { get; init; }

    public DateTimeOffset? LockoutEnd { get; init; }

    public DateTime? LastLogin { get; init; }

    public string LastLoginDevice { get; init; }

    public DateTime? LastPasswordChange { get; init; }

    public List<UserRoleDto> UserRoles { get; init; } = [];

    //Sessions: Count = 0
}
