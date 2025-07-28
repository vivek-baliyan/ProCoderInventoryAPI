using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class BusinessAddress : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string EntityType { get; set; } // "Customer", "Vendor"

    [Required]
    public int EntityId { get; set; }

    [Required]
    public AddressType AddressType { get; set; } = AddressType.Billing;

    [StringLength(100)]
    public string AddressLabel { get; set; }

    [Required]
    [StringLength(500)]
    public string AddressLine1 { get; set; }

    [StringLength(500)]
    public string AddressLine2 { get; set; }

    [StringLength(100)]
    public string City { get; set; }

    [StringLength(100)]
    public string State { get; set; }

    [StringLength(20)]
    public string PostalCode { get; set; }

    [StringLength(100)]
    public string Country { get; set; }

    [StringLength(50)]
    public string Region { get; set; }

    public bool IsDefault { get; set; } = false;

    public bool IsActive { get; set; } = true;

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}