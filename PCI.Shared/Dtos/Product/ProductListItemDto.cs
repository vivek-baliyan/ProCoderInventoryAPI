namespace PCI.Shared.Dtos.Product;

public class ProductListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Coupon { get; set; }
    public string Status { get; set; }
    public string SKU { get; set; }
    public ProductImageDto Image { get; set; }
}