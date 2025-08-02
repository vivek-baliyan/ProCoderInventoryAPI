using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class CustomerContact : BaseEntity
{
    public int CustomerId { get; set; }
    public ContactType ContactType { get; set; } = ContactType.Primary;

    // Contact Person Details
    public string Salutation { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Customer Customer { get; set; }
}