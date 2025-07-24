using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Product : BaseEntity
{
    [StringLength(100)]
    public string SKU { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(50)]
    public string ProductType { get; set; } = "goods"; // goods, service

    [StringLength(50)]
    public string ItemType { get; set; } = "inventory"; // inventory, sales, purchases, sales_and_purchases

    [StringLength(20)]
    public string Status { get; set; } = "active"; // active, inactive

    public bool IsActive { get; set; } = true;
    public bool IsTaxable { get; set; } = true;
    public bool TrackInventory { get; set; } = true;

    // Basic tracking settings
    public bool SerialNumberTracking { get; set; } = false;
    public bool BatchTracking { get; set; } = false;

    // Item Group relationship
    public int? ItemGroupId { get; set; }
    [ForeignKey("ItemGroupId")]
    public virtual ItemGroup ItemGroup { get; set; }

    // Brand relationship
    public int? BrandId { get; set; }
    [ForeignKey("BrandId")]
    public virtual Brand Brand { get; set; }

    // Product identification codes
    [StringLength(100)]
    public string ManufacturerPartNumber { get; set; }

    [StringLength(50)]
    public string UPC { get; set; }

    [StringLength(50)]
    public string EAN { get; set; }

    [StringLength(50)]
    public string ISBN { get; set; }

    // Accounting Integration (GL Accounts)
    public int? SalesAccountId { get; set; }
    public int? PurchaseAccountId { get; set; }  // COGS Account
    public int? InventoryAccountId { get; set; }  // Asset Account

    [ForeignKey("SalesAccountId")]
    public virtual ChartOfAccounts SalesAccount { get; set; }

    [ForeignKey("PurchaseAccountId")]
    public virtual ChartOfAccounts PurchaseAccount { get; set; }

    [ForeignKey("InventoryAccountId")]
    public virtual ChartOfAccounts InventoryAccount { get; set; }

    // Default pricing (for UI and default price list population)
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SellingPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CostPrice { get; set; }

    // Custom fields (JSON)
    [StringLength(2000)]
    public string CustomFields { get; set; }

    // Navigation properties
    public virtual ProductInventory ProductInventory { get; set; }
    public virtual ProductPhysical ProductPhysical { get; set; }
    public virtual ProductTax ProductTax { get; set; }
    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new HashSet<ProductAttributeValue>();
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new HashSet<ProductCategory>();
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
    public virtual ICollection<SerialNumber> SerialNumbers { get; set; } = new HashSet<SerialNumber>();
    public virtual ICollection<BatchNumber> BatchNumbers { get; set; } = new HashSet<BatchNumber>();
    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new HashSet<PurchaseOrderItem>();
    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new HashSet<SalesOrderItem>();
    public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new HashSet<PriceListItem>();
}