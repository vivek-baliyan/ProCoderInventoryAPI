namespace PCI.Shared.Dtos.SalesOrder;

public class SalesOrderListItemDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public string ReferenceNumber { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; }
}