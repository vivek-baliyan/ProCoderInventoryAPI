using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class Currency : BaseEntity
{
    [Required]
    [StringLength(3)]
    public string Code { get; set; } // ISO 4217 currency code (USD, EUR, INR, etc.)

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(10)]
    public string Symbol { get; set; }

    public int DecimalPlaces { get; set; } = 2;

    [Column(TypeName = "decimal(18,6)")]
    public decimal ExchangeRate { get; set; } = 1.00m; // Rate to base currency

    public bool IsBaseCurrency { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> ProductsWithSellingCurrency { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithCostCurrency { get; set; } = new HashSet<Product>();
}