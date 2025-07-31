using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class VendorDocument : BaseEntity
{
    public int VendorId { get; set; }

    public string DocumentName { get; set; }

    public string DocumentType { get; set; } // Contract, Certificate, License, Tax Document, etc.

    public string Description { get; set; }

    public string FilePath { get; set; }

    public string FileExtension { get; set; }

    public long FileSizeBytes { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsConfidential { get; set; } = false;

    public string UploadedBy { get; set; }

    public DateTime UploadedOn { get; set; }

    public string Notes { get; set; }

    // Navigation Properties
    public virtual Vendor Vendor { get; set; }
}