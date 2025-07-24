using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class BatchNumber : BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string BatchNumberValue { get; set; }

    public DateTime? ExpiryDate { get; set; }
    public DateTime? ManufactureDate { get; set; }

    public int QuantityAvailable { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Active"; // Active, Expired, Recalled

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}