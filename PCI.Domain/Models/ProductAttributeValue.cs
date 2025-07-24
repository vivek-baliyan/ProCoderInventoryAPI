using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductAttributeValue
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int ItemAttributeOptionId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("ItemAttributeOptionId")]
    public virtual ItemAttributeOption ItemAttributeOption { get; set; }
}