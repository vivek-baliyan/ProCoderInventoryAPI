using PCI.Domain.Common;
using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class UnitOfMeasure : BaseEntity
{
    [Required]
    [StringLength(20)]
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public UnitType UnitType { get; set; } = UnitType.Quantity;

    [StringLength(500)]
    public string Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int? OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> ProductsWithUnit { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithWeightUnit { get; set; } = new HashSet<Product>();
    public virtual ICollection<Product> ProductsWithDimensionUnit { get; set; } = new HashSet<Product>();
}