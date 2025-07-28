using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.SalesOrder;

public class UpdateSalesOrderDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }

    [StringLength(100)]
    public string ReferenceNumber { get; set; }
    
    [StringLength(100)]
    public string QuoteNumber { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public int? PriceListId { get; set; }

    [StringLength(1000)]
    public string Notes { get; set; }

    [StringLength(500)]
    public string BillingAddress { get; set; }

    public List<UpdateSalesOrderItemDto> SalesOrderItems { get; set; } = new List<UpdateSalesOrderItemDto>();

    // Payment information
    public UpdateSalesOrderPaymentDto Payment { get; set; }

    // Shipping information
    public UpdateSalesOrderShippingDto Shipping { get; set; }
}

public class UpdateSalesOrderItemDto
{
    public int? Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    public decimal DiscountPercentage { get; set; } = 0;

    public decimal DiscountAmount { get; set; } = 0;

    public int? TaxMasterId { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}

public class UpdateSalesOrderPaymentDto
{
    public int? Id { get; set; }

    [StringLength(50)]
    public string PaymentMethod { get; set; }

    [StringLength(50)]
    public string PaymentTerms { get; set; }

    public DateTime? DueDate { get; set; }

    [StringLength(500)]
    public string PaymentNotes { get; set; }
}

public class UpdateSalesOrderShippingDto
{
    public int? Id { get; set; }

    [StringLength(500)]
    public string ShippingAddress { get; set; }

    [StringLength(100)]
    public string ShippingMethod { get; set; }

    public decimal ShippingCost { get; set; } = 0;

    [StringLength(100)]
    public string TrackingNumber { get; set; }

    public DateTime? ShippingDate { get; set; }

    [StringLength(500)]
    public string ShippingNotes { get; set; }
}