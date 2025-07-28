using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrderDocument : BaseEntity
{
    public int SalesOrderId { get; set; }

    [Required]
    [StringLength(200)]
    public string DocumentName { get; set; }

    [Required]
    [StringLength(100)]
    public string DocumentType { get; set; } // Contract, Quote, Invoice, Shipping Label, etc.

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(500)]
    public string FilePath { get; set; }

    [StringLength(10)]
    public string FileExtension { get; set; }

    public long FileSizeBytes { get; set; }

    public bool IsActive { get; set; } = true;
    public bool IsConfidential { get; set; } = false;

    [StringLength(100)]
    public string UploadedBy { get; set; }

    public DateTime UploadedOn { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    // Navigation Properties
    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }
}