using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Product;

public class ProductListItemDto
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public ProductType ProductType { get; set; }
    public ProductStatus Status { get; set; }
    public bool IsReturnable { get; set; }
    public decimal? SellingPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public string BrandName { get; set; }
    public ProductImageDto Image { get; set; }
}