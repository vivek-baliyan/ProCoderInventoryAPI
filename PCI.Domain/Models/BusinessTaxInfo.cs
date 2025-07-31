using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class BusinessTaxInfo : BaseEntity
{
    public string EntityType { get; set; } // "Customer", "Vendor"

    public int EntityId { get; set; }

    public TaxType TaxType { get; set; }

    public string TaxNumber { get; set; }

    public string IssuingAuthority { get; set; }

    public string TaxCategory { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsPrimary { get; set; } = false;

    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}