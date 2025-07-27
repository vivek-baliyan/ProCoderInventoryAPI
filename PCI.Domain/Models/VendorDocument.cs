using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class VendorDocument : BaseEntity
{
    public int VendorId { get; set; }

    [Required]
    [StringLength(200)]
    public string DocumentName { get; set; }

    [Required]
    [StringLength(100)]
    public string DocumentType { get; set; } // Contract, Certificate, License, Tax Document, etc.

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(500)]
    public string FilePath { get; set; }

    [StringLength(10)]
    public string FileExtension { get; set; }

    public long FileSizeBytes { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsConfidential { get; set; } = false;

    [StringLength(100)]
    public string UploadedBy { get; set; }

    public DateTime UploadedOn { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }
}