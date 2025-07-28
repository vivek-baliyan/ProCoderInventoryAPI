using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class CustomerFinancial : BaseEntity
{
    [Required]
    public int CustomerId { get; set; }

    // Accounts Receivable (Customer owes us)
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal CreditLimit { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal OutstandingAmount { get; set; } = 0;

    // Payment Terms for customer payments
    public int PaymentTermDays { get; set; } = 30;

    // Sales History
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalesYTD { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalesLifetime { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal MinimumOrderValue { get; set; } = 0;

    public DateTime? LastSaleDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    // How customer pays us
    [StringLength(50)]
    public string PreferredPaymentMethod { get; set; } = "BankTransfer";

    // Credit Management (Customer-specific)
    public bool IsOnCreditHold { get; set; } = false;

    [StringLength(500)]
    public string CreditHoldReason { get; set; }

    public DateTime? CreditReviewDate { get; set; }

    // Discount Information
    [Column(TypeName = "decimal(5,2)")]
    public decimal DefaultDiscountPercentage { get; set; } = 0;

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    [ForeignKey("CustomerId")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }
}