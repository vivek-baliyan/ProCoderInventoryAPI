using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Vendor;

public record VendorDto
{
    public int Id { get; set; }
    public string VendorCode { get; set; }
    public string VendorName { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string WebsiteUrl { get; set; }

    // Vendor Classification
    public VendorType VendorType { get; set; }
    public VendorCategory Category { get; set; }
    public VendorStatus Status { get; set; }
    public string Industry { get; set; }

    // Vendor Hierarchy
    public int? ParentVendorId { get; set; }
    public string ParentVendorName { get; set; }
    public bool IsManufacturer { get; set; }
    public bool IsDropshipVendor { get; set; }

    // Financial Management
    public decimal CurrentBalance { get; set; }
    public decimal CreditLimit { get; set; }
    public int PaymentTermDays { get; set; }
    public decimal OutstandingAmount { get; set; }
    public DateTime? LastOrderDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public decimal TotalPurchasesYTD { get; set; }
    public decimal TotalPurchasesLifetime { get; set; }
    public VendorPaymentMethod PreferredPaymentMethod { get; set; }

    // Performance & Quality Tracking
    public decimal PerformanceRating { get; set; }
    public int OnTimeDeliveryPercentage { get; set; }
    public int QualityRating { get; set; }
    public DateTime? LastPerformanceReview { get; set; }
    public bool IsPreferredVendor { get; set; }
    public bool IsBlacklisted { get; set; }
    public string BlacklistReason { get; set; }

    // Tax & Banking Information
    public string TaxIdentificationNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankName { get; set; }
    public string BankBranch { get; set; }
    public string IFSCCode { get; set; }

    // Currency
    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencySymbol { get; set; }

    // Portal & Communication
    public string PortalAccessEmail { get; set; }
    public bool HasPortalAccess { get; set; }
    public string PreferredCommunicationMethod { get; set; }

    // Business Rules
    public bool RequiresPOApproval { get; set; }
    public decimal MinimumOrderValue { get; set; }

    // Status Management
    public DateTime? StatusChangedDate { get; set; }
    public string StatusChangeReason { get; set; }
    public string Notes { get; set; }

    // Audit Information
    public int OrganisationId { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
}