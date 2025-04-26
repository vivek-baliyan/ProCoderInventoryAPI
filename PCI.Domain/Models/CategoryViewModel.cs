using PCI.Shared.Common;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

/// <summary>
/// DTO for category creation or editing
/// </summary>
public class CategoryViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Page title is required")]
    [StringLength(100)]
    public string PageTitle { get; set; }

    [Required(ErrorMessage = "URL identifier is required")]
    [StringLength(255)]
    public string UrlIdentifier { get; set; }

    public string Description { get; set; }

    public int? ParentCategoryId { get; set; }

    public VisibilityStatus Status { get; set; } = VisibilityStatus.Published;

    public DateTime? PublishDate { get; set; }

    public DateTime? PublishTime { get; set; }

    // Holds image data for upload
    public string ImageBase64 { get; set; }

    // Cropping data
    public int? X { get; set; }
    public int? Y { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? Rotation { get; set; }
    public double? ScaleX { get; set; }
    public double? ScaleY { get; set; }
    public string AspectRatio { get; set; }
}
