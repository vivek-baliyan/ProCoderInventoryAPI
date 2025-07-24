using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductInventory : BaseEntity
{
    public int ProductId { get; set; }

    public int? UnitOfMeasureId { get; set; }

    public int? ReorderLevel { get; set; }
    public int? ReorderQuantity { get; set; }
    public int? MinimumStock { get; set; }
    public int? MaximumStock { get; set; }

    // Only store actual physical stock
    public int QuantityOnHand { get; set; } = 0;

    public int? OpeningStock { get; set; }
    public decimal? OpeningStockValue { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? AverageCost { get; set; }

    public bool IsSaleable { get; set; } = true;
    public bool IsPurchasable { get; set; } = true;
    public bool IsReturnable { get; set; } = true;

    public int? VendorId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("UnitOfMeasureId")]
    public virtual UnitOfMeasure UnitOfMeasure { get; set; }

    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }

    // Calculated properties (no database columns) 
    [NotMapped]
    public int QuantityAllocated => 
        Product?.SalesOrderItems?
            .Where(soi => soi.SalesOrder.Status == "Confirmed" && soi.QuantityAllocated > 0)
            .Sum(soi => soi.QuantityAllocated) ?? 0;

    [NotMapped] 
    public int QuantityAvailable => QuantityOnHand - QuantityAllocated;

    [NotMapped]
    public int QuantityOnOrder => 
        Product?.PurchaseOrderItems?
            .Where(poi => poi.PurchaseOrder.Status == "Confirmed")
            .Sum(poi => poi.QuantityOrdered - poi.QuantityReceived) ?? 0;
}