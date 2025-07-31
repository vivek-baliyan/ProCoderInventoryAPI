using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Customer;

public record CustomerDto
{
    public int Id { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string CompanyName { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string BillingAddress { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string WebsiteUrl { get; set; }
    public CustomerType CustomerType { get; set; }
    public int PaymentTermDays { get; set; }
    public decimal? CreditLimit { get; set; }
    public string TaxNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }
    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencySymbol { get; set; }
    public bool IsActive { get; set; }
    public string Notes { get; set; }
    public int OrganisationId { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public byte[] RowVersion { get; set; }
}