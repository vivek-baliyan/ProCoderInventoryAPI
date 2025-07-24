using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class AccountTransaction : BaseEntity
{
    public int AccountId { get; set; }

    [Required]
    [StringLength(50)]
    public string TransactionType { get; set; } // Sale, Purchase, StockAdjustment, etc.

    [Required]
    [StringLength(10)]
    public string EntryType { get; set; } // Debit, Credit

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; }

    [StringLength(100)]
    public string ReferenceNumber { get; set; }

    [StringLength(50)]
    public string ReferenceType { get; set; } // SalesOrder, PurchaseOrder, Invoice, etc.

    public int? ReferenceId { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    public int OrganisationId { get; set; }

    [ForeignKey("AccountId")]
    public virtual ChartOfAccounts Account { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }
}