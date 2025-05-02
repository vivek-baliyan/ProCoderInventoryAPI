using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Product;

public class CreateProductVariantDto
{
    [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
    public string TagName { get; set; }

    [Required(ErrorMessage = "Color is required")]
    [StringLength(20, ErrorMessage = "Color cannot exceed 20 characters")]
    public string Color { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number")]
    public int Quantity { get; set; }
}