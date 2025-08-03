using System.ComponentModel.DataAnnotations;
using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Customer;

public record CustomerContactDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public ContactType ContactType { get; set; } = ContactType.Primary;

    [StringLength(20, ErrorMessage = "Salutation cannot exceed 20 characters")]
    public string Salutation { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string PhoneNumber { get; set; }

    [StringLength(20, ErrorMessage = "Mobile number cannot exceed 20 characters")]
    public string MobileNumber { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;
}