using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductVariant : BaseEntity
{
    public int ProductId { get; set; }

    [StringLength(50)]
    public string TagName { get; set; }

    [StringLength(20)]
    public string Color { get; set; }

    public int Quantity { get; set; }

    // Size options
    public bool HasSizeXS { get; set; }
    public bool HasSizeS { get; set; }
    public bool HasSizeM { get; set; }
    public bool HasSizeL { get; set; }
    public bool HasSizeXL { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}
