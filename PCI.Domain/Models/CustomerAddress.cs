using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class CustomerAddress : BaseEntity
{
    public int CustomerId { get; set; }
    public AddressType AddressType { get; set; } = AddressType.Billing;

    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Customer Customer { get; set; }
}