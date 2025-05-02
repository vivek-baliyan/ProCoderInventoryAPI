using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; }

    public int OrganisationId { get; set; }

    public virtual Organisation Organisation { get; set; }

    public virtual ICollection<ProductTag> ProductTags { get; set; }
}
