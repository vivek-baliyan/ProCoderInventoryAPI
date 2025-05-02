using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductCategory
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}
