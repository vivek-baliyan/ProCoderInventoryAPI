namespace PCI.Shared.Dtos.SalesOrder;

public class SalesOrderFilterDto
{
    public string OrderNumber { get; set; }
    public string Status { get; set; }
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime? OrderDateFrom { get; set; }
    public DateTime? OrderDateTo { get; set; }
    public DateTime? ExpectedDeliveryDateFrom { get; set; }
    public DateTime? ExpectedDeliveryDateTo { get; set; }
    public decimal? TotalAmountFrom { get; set; }
    public decimal? TotalAmountTo { get; set; }
    public string ReferenceNumber { get; set; }
    public string QuoteNumber { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "OrderDate";
    public string SortDirection { get; set; } = "desc";
}