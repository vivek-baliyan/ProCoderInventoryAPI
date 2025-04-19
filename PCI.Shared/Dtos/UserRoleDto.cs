namespace PCI.Shared.Dtos;

public record UserRoleDto
{
    public string UserId { get; init; }
    public string RoleId { get; init; }
    public string RoleName { get; init; }
}