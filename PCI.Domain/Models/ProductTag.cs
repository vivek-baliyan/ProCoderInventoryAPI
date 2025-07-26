using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace PCI.Domain.Models;

public class ProductTag : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(20)]
    public string Color { get; set; }

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<ProductTagAssignment> ProductTagAssignments { get; set; } = new HashSet<ProductTagAssignment>();
}