using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrderItem : BaseEntity
{
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }

    // Fulfillment tracking
    public int QuantityAllocated { get; set; } = 0;
    public int QuantityPacked { get; set; } = 0;
    public int QuantityShipped { get; set; } = 0;
    public int QuantityDelivered { get; set; } = 0;

    [StringLength(500)]
    public string ItemNotes { get; set; }

    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}