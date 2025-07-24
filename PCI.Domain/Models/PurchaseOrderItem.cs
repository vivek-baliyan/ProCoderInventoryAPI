using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class PurchaseOrderItem : BaseEntity
{
    public int PurchaseOrderId { get; set; }
    public int ProductId { get; set; }

    [Required]
    public int QuantityOrdered { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }

    // Receipt tracking
    public int QuantityReceived { get; set; } = 0;
    public int QuantityPending { get; set; } = 0;

    public DateTime? ExpectedReceiptDate { get; set; }

    [StringLength(500)]
    public string ItemNotes { get; set; }

    [ForeignKey("PurchaseOrderId")]
    public virtual PurchaseOrder PurchaseOrder { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}