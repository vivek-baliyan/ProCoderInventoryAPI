using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class UnitOfMeasure : BaseEntity
{
    [Required]
    [StringLength(20)]
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(50)]
    public string UnitType { get; set; } // Weight, Volume, Quantity, Dimension

    [StringLength(500)]
    public string Description { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Product> ProductsWithUnit { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithWeightUnit { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithDimensionUnit { get; set; } = new HashSet<Product>();
}