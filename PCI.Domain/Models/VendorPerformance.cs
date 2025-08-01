using PCI.Domain.Common;

namespace PCI.Domain.Models;

public class VendorPerformance : BaseEntity
{
    public int VendorId { get; set; }

    // Performance Ratings
    public decimal PerformanceRating { get; set; } = 0; // 0-5 scale

    public int OnTimeDeliveryPercentage { get; set; } = 0; // 0-100

    public int QualityRating { get; set; } = 0; // 0-100 scale

    public int CommunicationRating { get; set; } = 0; // 0-100 scale

    public int PriceCompetitivenessRating { get; set; } = 0; // 0-100 scale

    // Review Information
    public DateTime? LastPerformanceReview { get; set; }

    public DateTime ReviewPeriodStart { get; set; }

    public DateTime ReviewPeriodEnd { get; set; }

    public string ReviewedBy { get; set; }

    // Performance Metrics
    public int TotalOrdersInPeriod { get; set; } = 0;

    public int OnTimeDeliveries { get; set; } = 0;

    public int LateDeliveries { get; set; } = 0;

    public int QualityIssues { get; set; } = 0;

    public int ResolvedComplaints { get; set; } = 0;

    public int UnresolvedComplaints { get; set; } = 0;

    // Status Flags
    public bool IsPreferredVendor { get; set; } = false;

    public bool IsBlacklisted { get; set; } = false;

    public string BlacklistReason { get; set; }

    public DateTime? BlacklistDate { get; set; }

    public string BlacklistedBy { get; set; }

    // Improvement Areas
    public string StrengthsNoted { get; set; }

    public string AreasForImprovement { get; set; }

    public string ActionPlan { get; set; }

    public string Notes { get; set; }

    // Navigation properties
    public virtual Vendor Vendor { get; set; }
}