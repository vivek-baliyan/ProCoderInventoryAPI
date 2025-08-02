namespace PCI.Shared.Dtos.Customer;

public record CustomerListItemDto
{
    public int Id { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string CustomerType { get; set; }
    public decimal? CreditLimit { get; set; }
    public bool IsActive { get; set; }
    public string CurrencyName { get; set; }
    public DateTime CreatedOn { get; set; }
}