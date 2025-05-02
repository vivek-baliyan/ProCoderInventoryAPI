using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductTag
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int TagId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("TagId")]
    public virtual Tag Tag { get; set; }
}
