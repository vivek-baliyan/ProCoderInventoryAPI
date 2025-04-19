namespace PCI.Shared.Dtos;

public record AssignUserRoleDto
{
    public string UserEmail { get; init; }
    public string RoleName { get; init; }
}
