using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class BusinessTaxInfo : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string EntityType { get; set; } // "Customer", "Vendor"

    [Required]
    public int EntityId { get; set; }

    [Required]
    public TaxType TaxType { get; set; }

    [Required]
    [StringLength(100)]
    public string TaxNumber { get; set; }

    [StringLength(200)]
    public string IssuingAuthority { get; set; }

    [StringLength(100)]
    public string TaxCategory { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsPrimary { get; set; } = false;

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}