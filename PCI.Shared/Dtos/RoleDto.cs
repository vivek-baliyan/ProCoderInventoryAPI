namespace PCI.Shared.Dtos;

public record RoleDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsDeleted { get; init; }
}