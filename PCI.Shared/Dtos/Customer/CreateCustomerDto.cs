using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Customer;

public record CreateCustomerDto
{
    [Required(ErrorMessage = "Customer code is required")]
    [StringLength(50, ErrorMessage = "Customer code cannot exceed 50 characters")]
    public string CustomerCode { get; set; }

    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(200, ErrorMessage = "Customer name cannot exceed 200 characters")]
    public string CustomerName { get; set; }

    [StringLength(200, ErrorMessage = "Company name cannot exceed 200 characters")]
    public string CompanyName { get; set; }

    [StringLength(100, ErrorMessage = "Contact person cannot exceed 100 characters")]
    public string ContactPerson { get; set; }

    [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [StringLength(20, ErrorMessage = "Mobile number cannot exceed 20 characters")]
    [Phone(ErrorMessage = "Invalid mobile number format")]
    public string MobileNumber { get; set; }

    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [StringLength(500, ErrorMessage = "Billing address cannot exceed 500 characters")]
    public string BillingAddress { get; set; }

    [StringLength(500, ErrorMessage = "Shipping address cannot exceed 500 characters")]
    public string ShippingAddress { get; set; }

    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string City { get; set; }

    [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
    public string State { get; set; }

    [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
    public string PostalCode { get; set; }

    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string Country { get; set; }

    [StringLength(200, ErrorMessage = "Website URL cannot exceed 200 characters")]
    [Url(ErrorMessage = "Invalid website URL format")]
    public string WebsiteUrl { get; set; }

    public CustomerType CustomerType { get; set; } = CustomerType.Individual;

    [Range(0, 365, ErrorMessage = "Payment term days must be between 0 and 365")]
    public int PaymentTermDays { get; set; } = 30;

    [Range(0, 999999999.99, ErrorMessage = "Credit limit must be between 0 and 999,999,999.99")]
    public decimal? CreditLimit { get; set; }

    [StringLength(50, ErrorMessage = "Tax number cannot exceed 50 characters")]
    public string TaxNumber { get; set; }

    [StringLength(50, ErrorMessage = "GST number cannot exceed 50 characters")]
    public string GSTNumber { get; set; }

    [StringLength(50, ErrorMessage = "PAN number cannot exceed 50 characters")]
    public string PANNumber { get; set; }

    public int? CurrencyId { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string Notes { get; set; }
}