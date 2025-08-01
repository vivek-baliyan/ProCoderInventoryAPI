using System.ComponentModel.DataAnnotations;
using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.BusinessAddress;

public record CreateBusinessAddressDto
{
    [Required(ErrorMessage = "Entity type is required")]
    [StringLength(50, ErrorMessage = "Entity type cannot exceed 50 characters")]
    public string EntityType { get; set; }

    [Required(ErrorMessage = "Entity ID is required")]
    public int EntityId { get; set; }

    public AddressType AddressType { get; set; } = AddressType.Billing;

    [StringLength(100, ErrorMessage = "Address label cannot exceed 100 characters")]
    public string AddressLabel { get; set; }

    [Required(ErrorMessage = "Address line 1 is required")]
    [StringLength(200, ErrorMessage = "Address line 1 cannot exceed 200 characters")]
    public string AddressLine1 { get; set; }

    [StringLength(200, ErrorMessage = "Address line 2 cannot exceed 200 characters")]
    public string AddressLine2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required")]
    [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
    public string State { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string Country { get; set; }

    [StringLength(100, ErrorMessage = "Region cannot exceed 100 characters")]
    public string Region { get; set; }

    public bool IsDefault { get; set; } = false;

    public bool IsActive { get; set; } = true;

    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    public string Notes { get; set; }
}