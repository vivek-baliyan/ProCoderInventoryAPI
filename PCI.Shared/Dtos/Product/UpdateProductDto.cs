namespace PCI.Shared.Dtos.Product;

public record UpdateProductDto : CreateProductDto
{
    public int Id { get; set; }
}
