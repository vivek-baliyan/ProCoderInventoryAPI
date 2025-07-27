using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class VendorContact : BaseEntity
{
    public int VendorId { get; set; }

    [Required]
    [StringLength(200)]
    public string ContactPersonName { get; set; }

    [StringLength(100)]
    public string JobTitle { get; set; }

    [StringLength(100)]
    public string Department { get; set; }

    [StringLength(100)]
    public string Role { get; set; } // Primary, Secondary, Technical, Financial, etc.

    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    [StringLength(20)]
    public string MobileNumber { get; set; }

    [StringLength(20)]
    public string Extension { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

    [StringLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }
}