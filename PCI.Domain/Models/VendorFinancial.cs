using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class VendorFinancial : BaseEntity
{
    public int VendorId { get; set; }

    // Accounts Payable (We owe vendor)
    public decimal CurrentBalance { get; set; } = 0;

    public decimal OutstandingAmount { get; set; } = 0;

    // Payment Terms for our payments to vendor
    public int PaymentTermDays { get; set; } = 30;

    // Purchase History
    public decimal TotalPurchasesYTD { get; set; } = 0;

    public decimal TotalPurchasesLifetime { get; set; } = 0;

    // Vendor's minimum order requirement
    public decimal MinimumOrderValue { get; set; } = 0;

    public DateTime? LastPurchaseDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    // How we pay vendor
    public string PreferredPaymentMethod { get; set; } = "BankTransfer";

    // Vendor Performance
    public decimal OnTimeDeliveryRate { get; set; } = 0;

    public decimal QualityRating { get; set; } = 0;

    // Discount Information from vendor
    public decimal EarlyPaymentDiscountPercentage { get; set; } = 0;

    public int EarlyPaymentDiscountDays { get; set; } = 10;

    // Credit Status (Are we on good terms with vendor)
    public bool IsBlacklisted { get; set; } = false;

    public string BlacklistReason { get; set; }

    public DateTime? LastReviewDate { get; set; }

    public string Notes { get; set; }

    // Navigation properties
    public virtual Vendor Vendor { get; set; }
}