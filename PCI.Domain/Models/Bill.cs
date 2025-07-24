using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Bill : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string BillNumber { get; set; }

    [Required]
    public DateTime BillDate { get; set; }

    public DateTime? DueDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Draft"; // Draft, Received, Paid, PartiallyPaid, Overdue, Cancelled

    public int VendorId { get; set; }
    public int OrganisationId { get; set; }

    // Reference to Purchase Order (if applicable)
    public int? PurchaseOrderId { get; set; }

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

    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }

    [ForeignKey("PurchaseOrderId")]
    public virtual PurchaseOrder PurchaseOrder { get; set; }

    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<BillItem> BillItems { get; set; } = new HashSet<BillItem>();
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; } = new HashSet<AccountTransaction>();
}