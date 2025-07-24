using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class Brand : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(200)]
    public string LogoUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
}