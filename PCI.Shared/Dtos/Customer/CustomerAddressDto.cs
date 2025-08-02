using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Customer;

public record CustomerAddressDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public AddressType AddressType { get; set; } = AddressType.Billing;

    [Required(ErrorMessage = "Address line 1 is required")]
    [StringLength(200, ErrorMessage = "Address line 1 cannot exceed 200 characters")]
    public string AddressLine1 { get; set; }

    [StringLength(200, ErrorMessage = "Address line 2 cannot exceed 200 characters")]
    public string AddressLine2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required")]
    public int StateId { get; set; }

    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public int CountryId { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;
}