using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class PriceListItem : BaseEntity
{
    public int PriceListId { get; set; }
    public int ProductId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    // Volume pricing support
    public int? MinQuantity { get; set; } = 1;
    public int? MaxQuantity { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DiscountPercentage { get; set; }

    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public bool IsActive { get; set; } = true;

    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}