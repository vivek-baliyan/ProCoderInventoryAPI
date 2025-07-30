using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class ProductItemGroup : BaseEntity
{
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int ItemGroupId { get; set; }
    public virtual ItemGroup ItemGroup { get; set; }

    public bool IsPrimary { get; set; } = false;
}