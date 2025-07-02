using PCI.Shared.Dtos.Category;

namespace PCI.Shared.Dtos.Product;

public record ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PageTitle { get; set; }
    public string UrlIdentifier { get; set; }
    public string Description { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal Price { get; set; }
    public string Coupon { get; set; }
    public string Status { get; set; }
    public DateTime? PublishDate { get; set; }
    public string SKU { get; set; }
    public int StockQuantity { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public bool HasSizeXS { get; set; }
    public bool HasSizeS { get; set; }
    public bool HasSizeM { get; set; }
    public bool HasSizeL { get; set; }
    public bool HasSizeXL { get; set; }
    public List<ProductVariantDto> Variants { get; set; } = [];
    public List<ProductImageDto> Images { get; set; } = [];
}
