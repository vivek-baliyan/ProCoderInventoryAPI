using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Customer;

public record UpdateCustomerDto
{
    [Required(ErrorMessage = "Customer ID is required")]
    public int Id { get; set; }

    public CustomerType CustomerType { get; set; } = CustomerType.Business;

    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string CompanyName { get; set; }

    [Required(ErrorMessage = "Customer display name is required")]
    [StringLength(200, ErrorMessage = "Customer display name cannot exceed 200 characters")]
    public string DisplayName { get; set; }

    [StringLength(200, ErrorMessage = "Website URL cannot exceed 200 characters")]
    [Url(ErrorMessage = "Invalid website URL format")]
    public string WebsiteUrl { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
    public string Status { get; set; } = "ACTIVE";

    [StringLength(20, ErrorMessage = "Salutation cannot exceed 20 characters")]
    public string Salutation { get; set; }

    [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
    public string FirstName { get; set; }

    [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
    public string LastName { get; set; }

    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    public string PhoneNumber { get; set; }

    [StringLength(20, ErrorMessage = "Mobile number cannot exceed 20 characters")]
    public string MobileNumber { get; set; }

    // Financial Information
    public decimal? CreditLimit { get; set; }

    [StringLength(100, ErrorMessage = "Payment terms cannot exceed 100 characters")]
    public string PaymentTerms { get; set; }

    // Tax Information (will be stored in CustomerTaxInfo)
    [StringLength(50, ErrorMessage = "PAN cannot exceed 50 characters")]
    public string Pan { get; set; }

    // Currency and Business Settings
    public int? CurrencyId { get; set; }
    public int? PriceListId { get; set; }
    public bool? AllowBackOrders { get; set; }
    public bool? SendStatements { get; set; }

    public bool IsActive { get; set; } = true;

    // Collections for related data updates
    public List<CustomerContactDto> ContactPersons { get; set; } = [];
    public List<CustomerAddressDto> Addresses { get; set; } = [];
}