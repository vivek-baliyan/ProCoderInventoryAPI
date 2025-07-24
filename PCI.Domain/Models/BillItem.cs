using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class BillItem : BaseEntity
{
    public int BillId { get; set; }
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TaxAmount { get; set; }

    [StringLength(500)]
    public string ItemNotes { get; set; }

    // Link back to purchase order item (if applicable)
    public int? PurchaseOrderItemId { get; set; }

    [ForeignKey("BillId")]
    public virtual Bill Bill { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("PurchaseOrderItemId")]
    public virtual PurchaseOrderItem PurchaseOrderItem { get; set; }
}