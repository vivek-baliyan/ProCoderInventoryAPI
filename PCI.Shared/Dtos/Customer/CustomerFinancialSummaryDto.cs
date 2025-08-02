using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Customer;

public record CustomerFinancialSummaryDto
{
    // Calculated financial metrics
    public decimal TotalSalesYTD { get; set; }
    public decimal TotalSalesLifetime { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal OutstandingAmount { get; set; }
    public decimal AvailableCredit { get; set; }
    public decimal CreditUtilization { get; set; }
    
    // Calculated date metrics
    public DateTime? LastSaleDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public int? DaysSinceLastSale { get; set; }
    public int? DaysSinceLastPayment { get; set; }
    
    // Static configuration
    public decimal CreditLimit { get; set; }
    public PaymentTerms PaymentTerms { get; set; }
    public int EffectivePaymentDays { get; set; }
    public CustomerPaymentMethod PreferredPaymentMethod { get; set; }
    public decimal MinimumOrderValue { get; set; }
    public decimal DefaultDiscountPercentage { get; set; }
    
    // Credit management
    public bool IsOnCreditHold { get; set; }
    public string CreditHoldReason { get; set; }
    public DateTime? CreditReviewDate { get; set; }
    public string Notes { get; set; }
}