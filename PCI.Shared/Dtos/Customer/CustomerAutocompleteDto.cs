namespace PCI.Shared.Dtos.Customer;

public record CustomerAutocompleteDto
{
    public int Id { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}