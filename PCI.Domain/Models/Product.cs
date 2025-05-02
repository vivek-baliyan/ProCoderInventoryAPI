using PCI.Domain.Common;
using PCI.Shared.Common.Enums;

namespace PCI.Domain.Models;


public class Product : BaseEntity
{
    public string Name { get; set; }

    public string PageTitle { get; set; }

    public string UrlIdentifier { get; set; }

    public string Description { get; set; }

    public decimal? OldPrice { get; set; }

    public decimal Price { get; set; }

    public string Coupon { get; set; }

    public VisibilityStatus Status { get; set; }

    // Publishing Schedule
    public DateTime? PublishDate { get; set; }

    // Inventory Info
    public string SKU { get; set; }

    public int StockQuantity { get; set; }

    public int OrganisationId { get; set; }

    public virtual Organisation Organisation { get; set; }

    // Categories (Many-to-Many relationship)
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }

    // Tags (Many-to-Many relationship)
    public virtual ICollection<ProductTag> ProductTags { get; set; }

    // Color and Size options
    public virtual ICollection<ProductVariant> ProductVariants { get; set; }

    // Images
    public virtual ICollection<ProductImage> ProductImages { get; set; }
}