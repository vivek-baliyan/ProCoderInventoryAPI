using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class TaxClassification : BaseEntity
{
    public string Code { get; set; }

    public string Description { get; set; }

    public string ClassificationType { get; set; } // HSN, SAC, UPC, SIC, NAICS, etc.

    public decimal? DefaultTaxRate { get; set; }

    public string Category { get; set; }

    public string CountryCode { get; set; } // ISO country code

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<ProductTax> ProductTaxes { get; set; } = new HashSet<ProductTax>();
}