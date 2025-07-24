using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Product;

public record CreateProductDto
{
    [StringLength(100, ErrorMessage = "SKU cannot exceed 100 characters")]
    public string SKU { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; }

    public ProductType ProductType { get; set; } = ProductType.Goods;

    public ProductStatus Status { get; set; } = ProductStatus.Active;

    public bool IsActive { get; set; } = true;

    public bool IsTaxable { get; set; } = true;

    public bool TrackInventory { get; set; } = true;

    public bool SerialNumberTracking { get; set; } = false;

    public bool BatchTracking { get; set; } = false;

    // Item Group and Brand
    public int? ItemGroupId { get; set; }
    public int? BrandId { get; set; }

    // Product identification codes
    [StringLength(100, ErrorMessage = "Manufacturer Part Number cannot exceed 100 characters")]
    public string ManufacturerPartNumber { get; set; }

    [StringLength(50, ErrorMessage = "UPC cannot exceed 50 characters")]
    public string UPC { get; set; }

    [StringLength(50, ErrorMessage = "EAN cannot exceed 50 characters")]
    public string EAN { get; set; }

    [StringLength(50, ErrorMessage = "ISBN cannot exceed 50 characters")]
    public string ISBN { get; set; }

    // Pricing
    [Range(0, 999999999.99, ErrorMessage = "Selling price must be between 0 and 999,999,999.99")]
    public decimal? SellingPrice { get; set; }

    [Range(0, 999999999.99, ErrorMessage = "Cost price must be between 0 and 999,999,999.99")]
    public decimal? CostPrice { get; set; }

    // GL Accounts
    public int? SalesAccountId { get; set; }
    public int? PurchaseAccountId { get; set; }
    public int? InventoryAccountId { get; set; }

    // Categories (list of category IDs)
    public List<int> CategoryIds { get; set; } = [];

    // Tags (list of tag names to create or associate)
    public List<string> Tags { get; set; } = [];

    // Custom fields (JSON)
    [StringLength(2000, ErrorMessage = "Custom fields cannot exceed 2000 characters")]
    public string CustomFields { get; set; }
}
