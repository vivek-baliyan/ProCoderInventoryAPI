using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SerialNumber : BaseEntity
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string SerialNumberValue { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Available"; // Available, Sold, Returned, etc.

    public DateTime? SoldDate { get; set; }
    public DateTime? ReturnedDate { get; set; }

    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
}