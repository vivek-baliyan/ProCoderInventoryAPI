using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(255)]
    public string ImagePath { get; set; }

    public bool IsMain { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}