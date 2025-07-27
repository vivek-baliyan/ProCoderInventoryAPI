using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Vendor;

public record UpdateVendorDto
{
    [Required(ErrorMessage = "Vendor ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Vendor code is required")]
    [StringLength(50, ErrorMessage = "Vendor code cannot exceed 50 characters")]
    public string VendorCode { get; set; }

    [Required(ErrorMessage = "Vendor name is required")]
    [StringLength(200, ErrorMessage = "Vendor name cannot exceed 200 characters")]
    public string VendorName { get; set; }

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

    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
    public string Address { get; set; }

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

    // Vendor Classification
    public VendorType VendorType { get; set; }
    public VendorCategory Category { get; set; }
    public VendorStatus Status { get; set; }

    [StringLength(100, ErrorMessage = "Industry cannot exceed 100 characters")]
    public string Industry { get; set; }

    // Vendor Hierarchy
    public int? ParentVendorId { get; set; }
    public bool IsManufacturer { get; set; }
    public bool IsDropshipVendor { get; set; }

    // Financial Management
    [Range(0, 999999999.99, ErrorMessage = "Credit limit must be between 0 and 999,999,999.99")]
    public decimal CreditLimit { get; set; }

    [Range(0, 365, ErrorMessage = "Payment term days must be between 0 and 365")]
    public int PaymentTermDays { get; set; }

    public VendorPaymentMethod PreferredPaymentMethod { get; set; }

    // Tax & Banking Information
    [StringLength(50, ErrorMessage = "Tax identification number cannot exceed 50 characters")]
    public string TaxIdentificationNumber { get; set; }

    [StringLength(50, ErrorMessage = "GST number cannot exceed 50 characters")]
    public string GSTNumber { get; set; }

    [StringLength(50, ErrorMessage = "PAN number cannot exceed 50 characters")]
    public string PANNumber { get; set; }

    [StringLength(100, ErrorMessage = "Bank account number cannot exceed 100 characters")]
    public string BankAccountNumber { get; set; }

    [StringLength(200, ErrorMessage = "Bank name cannot exceed 200 characters")]
    public string BankName { get; set; }

    [StringLength(200, ErrorMessage = "Bank branch cannot exceed 200 characters")]
    public string BankBranch { get; set; }

    [StringLength(20, ErrorMessage = "IFSC code cannot exceed 20 characters")]
    public string IFSCCode { get; set; }

    // Currency
    public int? CurrencyId { get; set; }

    // Portal & Communication
    [StringLength(100, ErrorMessage = "Portal access email cannot exceed 100 characters")]
    [EmailAddress(ErrorMessage = "Invalid portal access email format")]
    public string PortalAccessEmail { get; set; }

    public bool HasPortalAccess { get; set; }

    [StringLength(50, ErrorMessage = "Preferred communication method cannot exceed 50 characters")]
    public string PreferredCommunicationMethod { get; set; }

    // Business Rules
    public bool RequiresPOApproval { get; set; }

    [Range(0, 999999999.99, ErrorMessage = "Minimum order value must be between 0 and 999,999,999.99")]
    public decimal MinimumOrderValue { get; set; }

    [StringLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters")]
    public string Notes { get; set; }

    // Performance Tracking
    public bool IsPreferredVendor { get; set; }
    public bool IsBlacklisted { get; set; }

    [StringLength(500, ErrorMessage = "Blacklist reason cannot exceed 500 characters")]
    public string BlacklistReason { get; set; }

    // Performance Ratings
    [Range(0, 5, ErrorMessage = "Performance rating must be between 0 and 5")]
    public decimal PerformanceRating { get; set; }

    [Range(0, 100, ErrorMessage = "On time delivery percentage must be between 0 and 100")]
    public int OnTimeDeliveryPercentage { get; set; }

    [Range(0, 100, ErrorMessage = "Quality rating must be between 0 and 100")]
    public int QualityRating { get; set; }
}