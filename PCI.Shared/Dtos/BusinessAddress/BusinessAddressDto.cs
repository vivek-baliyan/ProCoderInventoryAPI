using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.BusinessAddress;

public record BusinessAddressDto
{
    public int Id { get; set; }
    public string EntityType { get; set; }
    public int EntityId { get; set; }
    public AddressType AddressType { get; set; }
    public string AddressLabel { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
}