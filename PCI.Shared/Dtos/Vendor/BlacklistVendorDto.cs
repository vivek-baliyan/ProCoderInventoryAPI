using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Vendor;

public record BlacklistVendorDto
{
    [Required(ErrorMessage = "Reason is required")]
    [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
    public string Reason { get; set; }
}