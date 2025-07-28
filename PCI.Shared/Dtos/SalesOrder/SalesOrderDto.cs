namespace PCI.Shared.Dtos.SalesOrder;

public class SalesOrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string ReferenceNumber { get; set; }
    public string QuoteNumber { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int OrganisationId { get; set; }
    public int? PriceListId { get; set; }
    public string PriceListName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public string BillingAddress { get; set; }
    
    // Workflow tracking
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? PackedDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string ModifiedBy { get; set; }

    public List<SalesOrderItemDto> SalesOrderItems { get; set; } = new List<SalesOrderItemDto>();
    public SalesOrderPaymentDto Payment { get; set; }
    public SalesOrderShippingDto Shipping { get; set; }
    public List<SalesOrderApprovalDto> Approvals { get; set; } = new List<SalesOrderApprovalDto>();
    public List<SalesOrderDocumentDto> Documents { get; set; } = new List<SalesOrderDocumentDto>();
}

public class SalesOrderItemDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSKU { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal LineTotal { get; set; }
    public int? TaxMasterId { get; set; }
    public string TaxName { get; set; }
    public decimal TaxAmount { get; set; }
    public string Description { get; set; }
}

public class SalesOrderPaymentDto
{
    public int Id { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentTerms { get; set; }
    public DateTime? DueDate { get; set; }
    public string PaymentNotes { get; set; }
}

public class SalesOrderShippingDto
{
    public int Id { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingMethod { get; set; }
    public decimal ShippingCost { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime? ShippingDate { get; set; }
    public string ShippingNotes { get; set; }
}

public class SalesOrderApprovalDto
{
    public int Id { get; set; }
    public string ApprovalLevel { get; set; }
    public string ApproverUserId { get; set; }
    public string ApproverName { get; set; }
    public string Status { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string Comments { get; set; }
}

public class SalesOrderDocumentDto
{
    public int Id { get; set; }
    public string DocumentType { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
    public DateTime UploadedDate { get; set; }
    public string UploadedBy { get; set; }
}