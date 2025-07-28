using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class VendorFinancial : BaseEntity
{
    [Required]
    public int VendorId { get; set; }

    // Accounts Payable (We owe vendor)
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal OutstandingAmount { get; set; } = 0;

    // Payment Terms for our payments to vendor
    public int PaymentTermDays { get; set; } = 30;

    // Purchase History
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchasesYTD { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPurchasesLifetime { get; set; } = 0;

    // Vendor's minimum order requirement
    [Column(TypeName = "decimal(18,2)")]
    public decimal MinimumOrderValue { get; set; } = 0;

    public DateTime? LastPurchaseDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    // How we pay vendor
    [StringLength(50)]
    public string PreferredPaymentMethod { get; set; } = "BankTransfer";

    // Vendor Performance
    [Column(TypeName = "decimal(3,2)")]
    public decimal OnTimeDeliveryRate { get; set; } = 0;

    [Column(TypeName = "decimal(3,2)")]
    public decimal QualityRating { get; set; } = 0;

    // Discount Information from vendor
    [Column(TypeName = "decimal(5,2)")]
    public decimal EarlyPaymentDiscountPercentage { get; set; } = 0;

    public int EarlyPaymentDiscountDays { get; set; } = 10;

    // Credit Status (Are we on good terms with vendor)
    public bool IsBlacklisted { get; set; } = false;

    [StringLength(500)]
    public string BlacklistReason { get; set; }

    public DateTime? LastReviewDate { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }
}