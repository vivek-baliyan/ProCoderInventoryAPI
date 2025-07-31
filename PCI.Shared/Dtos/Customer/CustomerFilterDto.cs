namespace PCI.Shared.Dtos.Customer;

public record CustomerFilterDto
{
    public string? SearchTerm { get; set; }
    public string? CustomerCode { get; set; }
    public string? CustomerName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? CustomerType { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public bool? IsActive { get; set; }
    public int? CurrencyId { get; set; }
    public decimal? MinCreditLimit { get; set; }
    public decimal? MaxCreditLimit { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }

    // Pagination
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // Sorting
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}