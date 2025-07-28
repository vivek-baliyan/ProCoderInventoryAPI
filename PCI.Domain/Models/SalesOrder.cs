using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrder : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string OrderNumber { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Draft"; // Draft, Confirmed, Packed, Shipped, Delivered, Closed, Void, OnHold

    public DateTime? ExpectedDeliveryDate { get; set; }

    // Reference fields for customer tracking
    [StringLength(100)]
    public string ReferenceNumber { get; set; }
    
    [StringLength(100)]
    public string QuoteNumber { get; set; }

    public int CustomerId { get; set; }
    public int OrganisationId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }

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

    // Payment integration moved to SalesOrderPayment entity

    [StringLength(1000)]
    public string Notes { get; set; }

    [StringLength(500)]
    public string BillingAddress { get; set; }

    // Shipping information moved to SalesOrderShipping entity

    // Approval workflow moved to SalesOrderApproval entity

    // Workflow tracking
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? PackedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<SalesOrderItem> SalesOrderItems { get; set; } = new HashSet<SalesOrderItem>();
    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new HashSet<StockTransaction>();
    
    // Normalized entities
    public virtual SalesOrderPayment SalesOrderPayment { get; set; }
    public virtual SalesOrderShipping SalesOrderShipping { get; set; }
    public virtual ICollection<SalesOrderApproval> SalesOrderApprovals { get; set; } = new HashSet<SalesOrderApproval>();
    
    // Document attachments support
    public virtual ICollection<SalesOrderDocument> SalesOrderDocuments { get; set; } = new HashSet<SalesOrderDocument>();
}