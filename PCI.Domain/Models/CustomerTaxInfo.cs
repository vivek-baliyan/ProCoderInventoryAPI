using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class CustomerTaxInfo : BaseEntity
{
    public int CustomerId { get; set; }
    public TaxType TaxType { get; set; } = TaxType.TaxIdentificationNumber;
    public string TaxNumber { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation property
    public virtual Customer Customer { get; set; }
}