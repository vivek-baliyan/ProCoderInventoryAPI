using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class TaxMaster : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string TaxCode { get; set; }

    [Required]
    [StringLength(200)]
    public string TaxName { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal TaxRate { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }

    [ForeignKey("OrganisationId")]
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}