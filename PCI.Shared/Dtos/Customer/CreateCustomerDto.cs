using System.ComponentModel.DataAnnotations;
using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Customer;

public record CreateCustomerDto
{
    // CustomerCode is now auto-generated, no longer required from frontend

    public CustomerType CustomerType { get; set; } = CustomerType.Business;

    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string CompanyName { get; set; }

    [Required(ErrorMessage = "Customer display name is required")]
    [StringLength(200, ErrorMessage = "Customer display name cannot exceed 200 characters")]
    public string CustomerName { get; set; }

    [StringLength(200, ErrorMessage = "Website URL cannot exceed 200 characters")]
    [Url(ErrorMessage = "Invalid website URL format")]
    public string WebsiteUrl { get; set; }

    // Primary Contact Information (will be stored in BusinessContact)
    [StringLength(20, ErrorMessage = "Salutation cannot exceed 20 characters")]
    public string Salutation { get; set; }

    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; }

    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; }

    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "Work phone cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid work phone format")]
    public string WorkPhone { get; set; }

    [StringLength(20, ErrorMessage = "Mobile cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid mobile format")]
    public string Mobile { get; set; }

    // Tax Information (will be stored in BusinessTaxInfo)
    [StringLength(50, ErrorMessage = "PAN cannot exceed 50 characters")]
    public string PAN { get; set; }

    public int? CurrencyId { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string Notes { get; set; }
}