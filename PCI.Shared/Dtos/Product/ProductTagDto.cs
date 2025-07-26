namespace PCI.Shared.Dtos.Product;

public record ProductTagDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public bool IsActive { get; set; }
}