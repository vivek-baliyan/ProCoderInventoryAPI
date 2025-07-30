using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class State : BaseEntity
{
    public string Name { get; set; }

    public string StateCode { get; set; }

    public string Type { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public int CountryId { get; set; }

    public virtual Country Country { get; set; }
}
