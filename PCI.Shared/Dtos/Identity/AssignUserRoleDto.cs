namespace PCI.Shared.Dtos.Identity;

public record AssignUserRoleDto
{
    public string UserEmail { get; init; }
    public string RoleName { get; init; }
}
