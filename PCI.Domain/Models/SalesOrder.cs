using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class SalesOrder : BaseEntity
{
    public string OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string Status { get; set; } = "Draft"; // Draft, Confirmed, Packed, Shipped, Delivered, Closed, Void, OnHold

    public DateTime? ExpectedDeliveryDate { get; set; }

    // Reference fields for customer tracking
    public string ReferenceNumber { get; set; }
    
    public string QuoteNumber { get; set; }

    public int CustomerId { get; set; }
    public int OrganisationId { get; set; }

    public virtual Customer Customer { get; set; }

    // Pricing integration
    public int? PriceListId { get; set; }
    public virtual PriceList PriceList { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    // Payment integration moved to SalesOrderPayment entity

    public string Notes { get; set; }

    public string BillingAddress { get; set; }

    // Shipping information moved to SalesOrderShipping entity

    // Approval workflow moved to SalesOrderApproval entity

    // Workflow tracking
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? PackedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

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