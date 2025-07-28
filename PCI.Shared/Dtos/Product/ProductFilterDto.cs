namespace PCI.Shared.Dtos.Product;

public class ProductFilterDto
{
    public string? SearchTerm { get; set; }
    public int? ProductType { get; set; }
    public int? Status { get; set; }
    public bool? IsActive { get; set; }
    public int? BrandId { get; set; }
    public int? ItemGroupId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? IsTaxable { get; set; }
    public bool? TrackInventory { get; set; }
    public List<int>? TagIds { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}