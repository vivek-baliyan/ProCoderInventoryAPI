using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class ProductPhysical : BaseEntity
{
    public int ProductId { get; set; }

    [Column(TypeName = "decimal(10,3)")]
    public decimal? Weight { get; set; }

    public int? WeightUnitId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Length { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Width { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Height { get; set; }

    public int? DimensionUnitId { get; set; }

    [Column(TypeName = "decimal(10,3)")]
    public decimal? Volume { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }

    [ForeignKey("WeightUnitId")]
    public virtual UnitOfMeasure WeightUnit { get; set; }

    [ForeignKey("DimensionUnitId")]
    public virtual UnitOfMeasure DimensionUnit { get; set; }
}