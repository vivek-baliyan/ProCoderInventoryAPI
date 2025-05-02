using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class State : BaseEntity
{
    public string Code { get; set; }

    public string Name { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; }
}
