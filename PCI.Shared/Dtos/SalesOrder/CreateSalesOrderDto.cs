using System.ComponentModel.DataAnnotations;

namespace PCI.Shared.Dtos.SalesOrder;

public class CreateSalesOrderDto
{
    [Required]
    [StringLength(50)]
    public string OrderNumber { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

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

    public List<CreateSalesOrderItemDto> SalesOrderItems { get; set; } = new List<CreateSalesOrderItemDto>();

    // Payment information
    public CreateSalesOrderPaymentDto Payment { get; set; }

    // Shipping information
    public CreateSalesOrderShippingDto Shipping { get; set; }
}

public class CreateSalesOrderItemDto
{
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

public class CreateSalesOrderPaymentDto
{
    [StringLength(50)]
    public string PaymentMethod { get; set; }

    [StringLength(50)]
    public string PaymentTerms { get; set; }

    public DateTime? DueDate { get; set; }

    [StringLength(500)]
    public string PaymentNotes { get; set; }
}

public class CreateSalesOrderShippingDto
{
    [StringLength(500)]
    public string ShippingAddress { get; set; }

    [StringLength(100)]
    public string ShippingMethod { get; set; }

    public decimal ShippingCost { get; set; } = 0;

    [StringLength(100)]
    public string TrackingNumber { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    [StringLength(500)]
    public string ShippingNotes { get; set; }
}