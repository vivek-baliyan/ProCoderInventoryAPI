using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Customer;

public record CustomerFinancialDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    // Payment Terms for customer payments
    public PaymentTerms PaymentTerms { get; set; } = PaymentTerms.Net30;

    // For custom payment terms only
    [Range(1, 365, ErrorMessage = "Custom payment term days must be between 1 and 365")]
    public int? CustomPaymentTermDays { get; set; }

    // How customer pays us
    public CustomerPaymentMethod PreferredPaymentMethod { get; set; } = CustomerPaymentMethod.BankTransfer;

    // Credit Management
    [Range(0, double.MaxValue, ErrorMessage = "Credit limit must be non-negative")]
    public decimal CreditLimit { get; set; } = 0;

    public bool IsOnCreditHold { get; set; } = false;

    [StringLength(500, ErrorMessage = "Credit hold reason cannot exceed 500 characters")]
    public string CreditHoldReason { get; set; }

    public DateTime? CreditReviewDate { get; set; }
}