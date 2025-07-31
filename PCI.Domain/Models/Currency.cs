using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class Currency : BaseEntity
{
    public string Code { get; set; } // ISO 4217 currency code (USD, EUR, INR, etc.)

    public string Name { get; set; }

    public string Symbol { get; set; }

    public int DecimalPlaces { get; set; } = 2;

    public decimal ExchangeRate { get; set; } = 1.00m; // Rate to base currency

    public bool IsBaseCurrency { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> ProductsWithSellingCurrency { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithCostCurrency { get; set; } = new HashSet<Product>();
}