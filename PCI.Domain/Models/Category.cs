using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

/// <summary>
/// Represents a product category in the system
/// </summary>
public class Category : BaseEntity
{
    public string Name { get; set; }

    public string PageTitle { get; set; }

    public string UrlIdentifier { get; set; }

    public string Description { get; set; }

    public VisibilityStatus Status { get; set; }

    public DateTime? PublishDate { get; set; }

    public int? ParentCategoryId { get; set; }

    [ForeignKey("ParentCategoryId")]
    public virtual Category ParentCategory { get; set; }

    public int OrganisationId { get; set; }

    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Category> ChildCategories { get; set; }
    public virtual ICollection<CategoryImage> CategoryImages { get; set; }
    public virtual ICollection<ProductCategory> ProductCategories { get; set; }
}