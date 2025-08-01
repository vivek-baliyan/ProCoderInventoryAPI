using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class CustomerFinancial : BaseEntity
{
    public int CustomerId { get; set; }

    // Accounts Receivable (Customer owes us)
    public decimal CurrentBalance { get; set; } = 0;

    public decimal CreditLimit { get; set; } = 0;

    public decimal OutstandingAmount { get; set; } = 0;

    // Payment Terms for customer payments
    public int PaymentTermDays { get; set; } = 30;
    public string PaymentTerms { get; set; } = "Due on Receipt";

    // Sales History
    public decimal TotalSalesYTD { get; set; } = 0;

    public decimal TotalSalesLifetime { get; set; } = 0;

    public decimal MinimumOrderValue { get; set; } = 0;

    public DateTime? LastSaleDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }

    // How customer pays us
    public string PreferredPaymentMethod { get; set; } = "BankTransfer";

    // Credit Management (Customer-specific)
    public bool IsOnCreditHold { get; set; } = false;

    public string CreditHoldReason { get; set; }

    public DateTime? CreditReviewDate { get; set; }

    // Discount Information
    public decimal DefaultDiscountPercentage { get; set; } = 0;

    public string Notes { get; set; }

    // Navigation properties
    public virtual Customer Customer { get; set; }
}