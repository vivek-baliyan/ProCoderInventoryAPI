using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.CustomerDocument;

public record CreateCustomerDocumentDto
{
    [Required(ErrorMessage = "Customer ID is required")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "File name is required")]
    [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters")]
    public string FileName { get; set; }

    [Required(ErrorMessage = "Original file name is required")]
    [StringLength(255, ErrorMessage = "Original file name cannot exceed 255 characters")]
    public string OriginalFileName { get; set; }

    [StringLength(10, ErrorMessage = "File extension cannot exceed 10 characters")]
    public string FileExtension { get; set; }

    [StringLength(100, ErrorMessage = "Content type cannot exceed 100 characters")]
    public string ContentType { get; set; }

    [Required(ErrorMessage = "File size is required")]
    [Range(1, long.MaxValue, ErrorMessage = "File size must be greater than 0")]
    public long FileSizeBytes { get; set; }

    [Required(ErrorMessage = "File path is required")]
    [StringLength(500, ErrorMessage = "File path cannot exceed 500 characters")]
    public string FilePath { get; set; }

    [StringLength(50, ErrorMessage = "Document type cannot exceed 50 characters")]
    public string DocumentType { get; set; } = "Other";

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; }

    [StringLength(100, ErrorMessage = "Uploaded by cannot exceed 100 characters")]
    public string UploadedBy { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string Notes { get; set; }
}