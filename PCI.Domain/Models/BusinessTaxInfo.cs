using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;

public class BusinessTaxInfo : BaseEntity
{
    public string EntityType { get; set; } // "Customer", "Vendor"

    public int EntityId { get; set; }

    public TaxType TaxType { get; set; }

    public string TaxNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsPrimary { get; set; } = false;

    public string Notes { get; set; }
}