using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class Country : BaseEntity
{
    public int OrganisationId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public List<State> States { get; set; }
}
