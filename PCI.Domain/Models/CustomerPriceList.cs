using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class CustomerPriceList : BaseEntity
{
    public int CustomerId { get; set; }
    public int PriceListId { get; set; }
    public bool IsDefault { get; set; } = false;
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }

    public virtual Customer Customer { get; set; }
}