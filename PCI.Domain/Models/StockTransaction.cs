using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class StockTransaction : BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(50)]
    public string TransactionType { get; set; } // SalesOrderConfirm, SalesOrderShip, PurchaseOrderReceive, StockAdjustment, Transfer

    [Required]
    [StringLength(20)]
    public string MovementType { get; set; } // In, Out, Allocation, DeAllocation

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? UnitCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TotalValue { get; set; }

    [StringLength(100)]
    public string ReferenceNumber { get; set; }

    [StringLength(50)]
    public string ReferenceType { get; set; } // SalesOrder, PurchaseOrder, StockAdjustment

    public int? ReferenceId { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

    public int? WarehouseId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    // Dynamic foreign keys based on ReferenceType
    public virtual SalesOrder SalesOrder { get; set; }
    public virtual PurchaseOrder PurchaseOrder { get; set; }
}