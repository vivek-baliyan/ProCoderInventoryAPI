using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class BusinessContact : BaseEntity
{
    public string EntityType { get; set; } // "Customer", "Vendor"

    public int EntityId { get; set; }

    public ContactType ContactType { get; set; } = ContactType.Primary;

    public string ContactPersonName { get; set; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    public string Role { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string MobileNumber { get; set; }

    public string Extension { get; set; }

    public string LinkedInProfile { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}