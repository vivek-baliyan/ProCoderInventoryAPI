using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Services.Interfaces;

public interface ICustomerFinancialCalculationService
{
    /// <summary>
    /// Calculates comprehensive financial summary for a customer
    /// </summary>
    Task<CustomerFinancialSummaryDto> CalculateFinancialSummaryAsync(int customerId);
    
    /// <summary>
    /// Calculates current balance (total invoiced - total paid)
    /// </summary>
    Task<decimal> CalculateCurrentBalanceAsync(int customerId);
    
    /// <summary>
    /// Calculates outstanding amount (unpaid invoices)
    /// </summary>
    Task<decimal> CalculateOutstandingAmountAsync(int customerId);
    
    /// <summary>
    /// Calculates total sales for current year
    /// </summary>
    Task<decimal> CalculateTotalSalesYTDAsync(int customerId);
    
    /// <summary>
    /// Calculates total sales for customer lifetime
    /// </summary>
    Task<decimal> CalculateTotalSalesLifetimeAsync(int customerId);
    
    /// <summary>
    /// Gets the date of the last sale for customer
    /// </summary>
    Task<DateTime?> GetLastSaleDateAsync(int customerId);
    
    /// <summary>
    /// Gets the date of the last payment from customer
    /// </summary>
    Task<DateTime?> GetLastPaymentDateAsync(int customerId);
    
    /// <summary>
    /// Calculates available credit (credit limit - current balance)
    /// </summary>
    Task<decimal> CalculateAvailableCreditAsync(int customerId);
    
    /// <summary>
    /// Calculates credit utilization percentage
    /// </summary>
    Task<decimal> CalculateCreditUtilizationAsync(int customerId);
}