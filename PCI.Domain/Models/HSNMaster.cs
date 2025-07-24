using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class HSNMaster : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string HSNCode { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal? DefaultTaxRate { get; set; }

    [StringLength(200)]
    public string Category { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}