using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class CustomerDocument : BaseEntity
{
    public int CustomerId { get; set; }

    public string FileName { get; set; }

    public string OriginalFileName { get; set; }

    public string FileExtension { get; set; }

    public string ContentType { get; set; }

    public long FileSizeBytes { get; set; }

    public string FilePath { get; set; }

    public string DocumentType { get; set; } // "Contract", "License", "Certificate", "Other"

    public string Description { get; set; }

    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

    public string UploadedBy { get; set; }

    public bool IsActive { get; set; } = true;

    public string Notes { get; set; }

    // Navigation properties
    public virtual Customer Customer { get; set; }
}