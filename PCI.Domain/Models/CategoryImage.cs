using PCI.Domain.Common;

namespace PCI.Domain.Models;

/// <summary>
/// Represents an image associated with a category
/// </summary>
public class CategoryImage : BaseEntity
{
    public string ImagePath { get; set; }

    public string AltText { get; set; }

    // Properties for cropped image data
    public int? X { get; set; }
    public int? Y { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int? Rotation { get; set; }
    public double? ScaleX { get; set; }
    public double? ScaleY { get; set; }

    // Aspect ratio used for cropping
    public string AspectRatio { get; set; }

    public bool IsPrimary { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }
}
