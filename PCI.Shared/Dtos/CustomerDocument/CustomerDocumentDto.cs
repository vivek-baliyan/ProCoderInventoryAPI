namespace PCI.Shared.Dtos.CustomerDocument;

public record CustomerDocumentDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public string FileExtension { get; set; }
    public string ContentType { get; set; }
    public long FileSizeBytes { get; set; }
    public string FilePath { get; set; }
    public string DocumentType { get; set; }
    public string Description { get; set; }
    public DateTime UploadedDate { get; set; }
    public string UploadedBy { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}