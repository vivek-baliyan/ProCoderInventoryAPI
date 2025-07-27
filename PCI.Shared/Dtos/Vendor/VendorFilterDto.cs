using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Vendor;

public record VendorFilterDto
{
    public string? SearchTerm { get; set; }
    public string? VendorCode { get; set; }
    public string? VendorName { get; set; }
    public string? CompanyName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public VendorType? VendorType { get; set; }
    public VendorCategory? Category { get; set; }
    public VendorStatus? Status { get; set; }
    public string? Industry { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public int? CurrencyId { get; set; }
    public bool? IsPreferredVendor { get; set; }
    public bool? IsBlacklisted { get; set; }
    public bool? IsManufacturer { get; set; }
    public bool? IsDropshipVendor { get; set; }
    public bool? HasPortalAccess { get; set; }
    public bool? RequiresPOApproval { get; set; }
    
    // Financial Filters
    public decimal? MinCreditLimit { get; set; }
    public decimal? MaxCreditLimit { get; set; }
    public decimal? MinCurrentBalance { get; set; }
    public decimal? MaxCurrentBalance { get; set; }
    public decimal? MinOutstandingAmount { get; set; }
    public decimal? MaxOutstandingAmount { get; set; }
    
    // Performance Filters
    public decimal? MinPerformanceRating { get; set; }
    public decimal? MaxPerformanceRating { get; set; }
    public int? MinOnTimeDeliveryPercentage { get; set; }
    public int? MaxOnTimeDeliveryPercentage { get; set; }
    public int? MinQualityRating { get; set; }
    public int? MaxQualityRating { get; set; }
    
    // Date Filters
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? LastOrderFrom { get; set; }
    public DateTime? LastOrderTo { get; set; }
    public DateTime? LastPaymentFrom { get; set; }
    public DateTime? LastPaymentTo { get; set; }
    
    // Pagination
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    // Sorting
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}