using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class BusinessContact : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string EntityType { get; set; } // "Customer", "Vendor"

    [Required]
    public int EntityId { get; set; }

    [Required]
    public ContactType ContactType { get; set; } = ContactType.Primary;

    [Required]
    [StringLength(200)]
    public string ContactPersonName { get; set; }

    [StringLength(100)]
    public string JobTitle { get; set; }

    [StringLength(100)]
    public string Department { get; set; }

    [StringLength(100)]
    public string Role { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(20)]
    public string MobileNumber { get; set; }

    [StringLength(20)]
    public string Extension { get; set; }

    [StringLength(100)]
    public string LinkedInProfile { get; set; }

    public bool IsPrimary { get; set; } = false;

    public bool IsActive { get; set; } = true;

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Organisation Organisation { get; set; }
}