using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class BusinessAddress : BaseEntity
{
    public string EntityType { get; set; } // "Customer", "Vendor"

    public int EntityId { get; set; }

    public AddressType AddressType { get; set; } = AddressType.Billing;

    public string AddressLabel { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }

    public string Region { get; set; }

    public bool IsDefault { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}