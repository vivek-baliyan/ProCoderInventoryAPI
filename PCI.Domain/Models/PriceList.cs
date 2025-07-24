using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class PriceList : BaseEntity
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; } // Sales, Purchase

    [StringLength(20)]
    public string PricingMethod { get; set; } = "Individual"; // Individual, Markup, Markdown

    [Column(TypeName = "decimal(5,2)")]
    public decimal? MarkupPercentage { get; set; }

    [StringLength(10)]
    public string Currency { get; set; } = "USD";

    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; } = false;

    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new HashSet<PriceListItem>();
    public virtual ICollection<CustomerPriceList> CustomerPriceLists { get; set; } = new HashSet<CustomerPriceList>();
    public virtual ICollection<VendorPriceList> VendorPriceLists { get; set; } = new HashSet<VendorPriceList>();
}