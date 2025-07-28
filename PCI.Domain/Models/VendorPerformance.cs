using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class VendorPerformance : BaseEntity
{
    [Required]
    public int VendorId { get; set; }

    // Performance Ratings
    [Column(TypeName = "decimal(3,2)")]
    public decimal PerformanceRating { get; set; } = 0; // 0-5 scale

    public int OnTimeDeliveryPercentage { get; set; } = 0; // 0-100

    public int QualityRating { get; set; } = 0; // 0-100 scale

    public int CommunicationRating { get; set; } = 0; // 0-100 scale

    public int PriceCompetitivenessRating { get; set; } = 0; // 0-100 scale

    // Review Information
    public DateTime? LastPerformanceReview { get; set; }

    public DateTime ReviewPeriodStart { get; set; }

    public DateTime ReviewPeriodEnd { get; set; }

    [StringLength(100)]
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

    [StringLength(500)]
    public string BlacklistReason { get; set; }

    public DateTime? BlacklistDate { get; set; }

    [StringLength(100)]
    public string BlacklistedBy { get; set; }

    // Improvement Areas
    [StringLength(1000)]
    public string StrengthsNoted { get; set; }

    [StringLength(1000)]
    public string AreasForImprovement { get; set; }

    [StringLength(1000)]
    public string ActionPlan { get; set; }

    [StringLength(500)]
    public string Notes { get; set; }

    // Multi-tenancy
    public int OrganisationId { get; set; }

    // Navigation properties
    public virtual Vendor Vendor { get; set; }
    public virtual Organisation Organisation { get; set; }
}