using PCI.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCI.Domain.Models;

public class SalesOrderShipping : BaseEntity
{
    [Required]
    public int SalesOrderId { get; set; }

    [StringLength(100)]
    public string ShippingMethod { get; set; }

    [StringLength(100)]
    public string TrackingNumber { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ShippingCost { get; set; } = 0;

    [StringLength(100)]
    public string CarrierName { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    [StringLength(500)]
    public string ShippingAddress { get; set; }

    [StringLength(20)]
    public string ShippingStatus { get; set; } = "Pending"; // Pending, Dispatched, InTransit, Delivered

    [StringLength(500)]
    public string ShippingNotes { get; set; }

    public bool IsDropShipment { get; set; } = false;

    // Navigation properties
    [ForeignKey("SalesOrderId")]
    public virtual SalesOrder SalesOrder { get; set; }
}