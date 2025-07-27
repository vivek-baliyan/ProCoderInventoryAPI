using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Vendor;

public record VendorListItemDto
{
    public int Id { get; set; }
    public string VendorCode { get; set; }
    public string VendorName { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public VendorType VendorType { get; set; }
    public VendorCategory Category { get; set; }
    public VendorStatus Status { get; set; }
    public string Industry { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal OutstandingAmount { get; set; }
    public bool IsPreferredVendor { get; set; }
    public bool IsBlacklisted { get; set; }
    public decimal PerformanceRating { get; set; }
    public int OnTimeDeliveryPercentage { get; set; }
    public DateTime? LastOrderDate { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public string CurrencyName { get; set; }
    public DateTime CreatedOn { get; set; }
}