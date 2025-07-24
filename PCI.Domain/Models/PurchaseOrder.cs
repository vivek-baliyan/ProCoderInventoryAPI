using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class PurchaseOrder : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string PONumber { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Draft"; // Draft, Confirmed, PartiallyReceived, Received, Closed, Cancelled

    public DateTime? ExpectedDeliveryDate { get; set; }

    public int VendorId { get; set; }
    public int OrganisationId { get; set; }

    // Pricing integration
    public int? PriceListId { get; set; }
    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; }

    [StringLength(500)]
    public string DeliveryAddress { get; set; }

    [StringLength(100)]
    public string PaymentTerms { get; set; }

    // Workflow tracking
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? ReceivedDate { get; set; }

    // Reference to originating Sales Order (if converted)
    public int? OriginatingSalesOrderId { get; set; }

    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    [ForeignKey("OriginatingSalesOrderId")]
    public virtual SalesOrder OriginatingSalesOrder { get; set; }

    public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new HashSet<PurchaseOrderItem>();
    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new HashSet<StockTransaction>();
}