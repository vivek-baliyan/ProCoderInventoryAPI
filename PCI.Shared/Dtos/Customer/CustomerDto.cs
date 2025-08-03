using PCI.Shared.Common.Enums;

namespace PCI.Shared.Dtos.Customer;

public record CustomerDto
{
    public int Id { get; set; }
    public string CustomerCode { get; set; }
    public string DisplayName { get; set; }
    public string CompanyName { get; set; }
    public string WebsiteUrl { get; set; }
    public CustomerType CustomerType { get; set; }
    public string Status { get; set; }

    public CustomerFinancialDto Financial { get; set; }

    public int? CurrencyId { get; set; }
    public string CurrencyName { get; set; }
    public string CurrencySymbol { get; set; }

    public int? PriceListId { get; set; }
    public string PriceListName { get; set; }

    public bool? AllowBackOrders { get; set; }
    public bool? SendStatements { get; set; }

    public bool IsActive { get; set; }
    public int OrganisationId { get; set; }

    public CustomerContactDto PrimaryContact { get; set; }
    public CustomerAddressDto BillingAddress { get; set; }
    public CustomerAddressDto ShippingAddress { get; set; }
    public string TaxNumber { get; set; }
    public string GSTNumber { get; set; }
    public string PANNumber { get; set; }

    // Collections
    public List<CustomerContactDto> ContactPersons { get; set; } = [];
    public List<CustomerAddressDto> Addresses { get; set; } = [];
    public List<CustomerTaxInfoDto> TaxInfos { get; set; } = [];
}