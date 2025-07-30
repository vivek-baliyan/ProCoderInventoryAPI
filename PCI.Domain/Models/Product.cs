using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class Product : BaseEntity
{
    public string SKU { get; set; }
    public string Name { get; set; }
    public ProductType ProductType { get; set; } = ProductType.Goods;
    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public bool IsReturnable { get; set; } = true;
    public bool TrackInventory { get; set; } = true;

    // Relationships

    public int? BrandId { get; set; }
    public virtual Brand Brand { get; set; }

    // Product identification codes
    public string ManufacturerPartNumber { get; set; }
    public string UPC { get; set; }
    public string EAN { get; set; }
    public string ISBN { get; set; }

    // GL Accounts for accounting integration
    public int? SalesAccountId { get; set; }
    public int? PurchaseAccountId { get; set; }
    public int? InventoryAccountId { get; set; }
    public virtual GLAccount SalesAccount { get; set; }
    public virtual GLAccount PurchaseAccount { get; set; }
    public virtual GLAccount InventoryAccount { get; set; }

    // Pricing
    public decimal? SellingPrice { get; set; }
    public decimal? CostPrice { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }

    // Navigation properties
    public virtual ProductInventory ProductInventory { get; set; }
    public virtual ProductPhysical ProductPhysical { get; set; }
    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new HashSet<ProductAttributeValue>();
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new HashSet<PurchaseOrderItem>();
    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new HashSet<SalesOrderItem>();
    public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new HashSet<PriceListItem>();
    public virtual ICollection<ProductTagAssignment> ProductTagAssignments { get; set; } = new HashSet<ProductTagAssignment>();
    public virtual ICollection<ProductItemGroup> ProductItemGroups { get; set; } = new HashSet<ProductItemGroup>();
}

