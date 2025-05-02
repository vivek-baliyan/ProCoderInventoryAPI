using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Product;

public record CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [StringLength(100, ErrorMessage = "Page title cannot exceed 100 characters")]
    public string PageTitle { get; set; }

    [StringLength(100, ErrorMessage = "URL identifier cannot exceed 100 characters")]
    [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "URL identifier can only contain letters, numbers, and hyphens")]
    public string UrlIdentifier { get; set; }

    [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters")]
    public string Description { get; set; }

    // Pricing Info
    [Range(0, 999999.99, ErrorMessage = "Old price must be between 0 and 999,999.99")]
    public decimal? OldPrice { get; set; }

    [Required(ErrorMessage = "Product price is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
    public decimal Price { get; set; }

    [StringLength(50, ErrorMessage = "Coupon code cannot exceed 50 characters")]
    public string Coupon { get; set; }

    // Visibility Status
    [Required(ErrorMessage = "Visibility status is required")]
    public int Status { get; set; }

    // Publishing Schedule
    [DataType(DataType.Date)]
    public DateTime? PublishDate { get; set; }

    [DataType(DataType.Time)]
    public TimeSpan? PublishTime { get; set; }

    // Inventory Info
    [StringLength(50, ErrorMessage = "SKU cannot exceed 50 characters")]
    public string SKU { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number")]
    public int StockQuantity { get; set; }

    // Categories (list of category IDs)
    public List<int> CategoryIds { get; set; } = [];

    // Tags (list of tag names to create or associate)
    public List<string> Tags { get; set; } = [];

    // Size options
    public bool HasSizeXS { get; set; }
    public bool HasSizeS { get; set; }
    public bool HasSizeM { get; set; }
    public bool HasSizeL { get; set; }
    public bool HasSizeXL { get; set; }

    // Product variants
    public List<CreateProductVariantDto> Variants { get; set; } = [];
}
