using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class CustomerFinancial : BaseEntity
{
    public int CustomerId { get; set; }

    // Payment Terms for customer payments
    public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Net30;

    // For custom payment terms only
    public int? CustomPaymentTermDays { get; set; }

    // How customer pays us
    public CustomerPaymentMethod PreferredPaymentMethod { get; set; } = CustomerPaymentMethod.BankTransfer;

    // Credit Management
    public decimal CreditLimit { get; set; } = 0;

    public bool IsOnCreditHold { get; set; } = false;

    public string CreditHoldReason { get; set; }

    public DateTime? CreditReviewDate { get; set; }

    // Navigation properties
    public virtual Customer Customer { get; set; }
}