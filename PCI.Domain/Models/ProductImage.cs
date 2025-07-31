using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string ImagePath { get; set; }

    public bool IsMain { get; set; }

    public virtual Product Product { get; set; }
}