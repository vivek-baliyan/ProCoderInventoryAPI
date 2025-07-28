using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Vendor;

public record UpdateVendorPerformanceDto
{
    [Range(0, 5, ErrorMessage = "Performance rating must be between 0 and 5")]
    public decimal PerformanceRating { get; set; }

    [Range(0, 100, ErrorMessage = "On time delivery percentage must be between 0 and 100")]
    public int OnTimeDeliveryPercentage { get; set; }

    [Range(0, 100, ErrorMessage = "Quality rating must be between 0 and 100")]
    public int QualityRating { get; set; }
}