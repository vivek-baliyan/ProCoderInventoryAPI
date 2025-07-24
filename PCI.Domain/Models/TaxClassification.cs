using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class TaxClassification : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(50)]
    public string ClassificationType { get; set; } // HSN, SAC, UPC, SIC, NAICS, etc.

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DefaultTaxRate { get; set; }

    [StringLength(200)]
    public string Category { get; set; }

    [StringLength(10)]
    public string CountryCode { get; set; } // ISO country code

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<ProductTax> ProductTaxes { get; set; } = new HashSet<ProductTax>();
}