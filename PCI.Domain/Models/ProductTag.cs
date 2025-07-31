using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class ProductTag : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public bool IsActive { get; set; } = true;

    public int OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<ProductTagAssignment> ProductTagAssignments { get; set; } = new HashSet<ProductTagAssignment>();
}