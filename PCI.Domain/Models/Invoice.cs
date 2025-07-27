using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Invoice : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string InvoiceNumber { get; set; }

    [Required]
    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Draft"; // Draft, Sent, Paid, PartiallyPaid, Overdue, Cancelled

    public int CustomerId { get; set; }
    public int OrganisationId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }

    // Reference to Sales Order (if applicable)
    public int? SalesOrderId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal AmountPaid { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal AmountDue { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; }

    [StringLength(100)]
    public string PaymentTerms { get; set; }

    // Pricing integration
    public int? PriceListId { get; set; }

    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }

    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}