using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; }

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string Status { get; set; } = "Draft"; // Draft, Sent, Paid, PartiallyPaid, Overdue, Cancelled

    public int CustomerId { get; set; }
    public int OrganisationId { get; set; }

    public virtual Customer Customer { get; set; }

    // Reference to Sales Order (if applicable)
    public int? SalesOrderId { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal AmountPaid { get; set; } = 0;

    public decimal AmountDue { get; set; }

    public string Notes { get; set; }

    public string PaymentTerms { get; set; }

    // Pricing integration
    public int? PriceListId { get; set; }

    public virtual SalesOrder SalesOrder { get; set; }

    public virtual PriceList PriceList { get; set; }

    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}