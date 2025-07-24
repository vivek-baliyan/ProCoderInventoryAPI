using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Category;

namespace PCI.Shared.Dtos.Product;

public record ProductDto
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProductType ProductType { get; set; }
    public ProductStatus Status { get; set; }
    public bool IsActive { get; set; }
    public bool IsTaxable { get; set; }
    public bool TrackInventory { get; set; }
    public bool SerialNumberTracking { get; set; }
    public bool BatchTracking { get; set; }
    public string ManufacturerPartNumber { get; set; }
    public string UPC { get; set; }
    public string EAN { get; set; }
    public string ISBN { get; set; }
    public decimal? SellingPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public List<ProductImageDto> Images { get; set; } = [];
}
