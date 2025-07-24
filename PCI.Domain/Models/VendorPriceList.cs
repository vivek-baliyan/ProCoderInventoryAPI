using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class VendorPriceList
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public int PriceListId { get; set; }
    public bool IsDefault { get; set; } = false;
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    [ForeignKey("VendorId")]
    public virtual Vendor Vendor { get; set; }

    [ForeignKey("PriceListId")]
    public virtual PriceList PriceList { get; set; }
}