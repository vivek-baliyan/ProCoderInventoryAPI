using PCI.Shared.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.Customer;

public record CustomerTaxInfoDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public TaxType TaxType { get; set; } = TaxType.TaxIdentificationNumber;
    
    [Required(ErrorMessage = "Tax number is required")]
    [StringLength(50, ErrorMessage = "Tax number cannot exceed 50 characters")]
    public string TaxNumber { get; set; }

    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;
}