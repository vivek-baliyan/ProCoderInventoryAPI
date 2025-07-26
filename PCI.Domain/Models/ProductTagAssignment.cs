using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductTagAssignment
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int ProductTagId { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("ProductTagId")]
    public virtual ProductTag ProductTag { get; set; }
}