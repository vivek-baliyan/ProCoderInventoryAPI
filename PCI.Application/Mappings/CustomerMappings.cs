using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Mappings;

public static class CustomerMappings
{
    public static void Configure(TypeAdapterConfig config)
    {
        config.ForType<CreateCustomerDto, Customer>()
            .Map(dst => dst.CustomerType, src => (CustomerType)src.CustomerType);

        config.ForType<UpdateCustomerDto, Customer>()
            .Map(dst => dst.CustomerType, src => src.CustomerType);

        config.ForType<Customer, CustomerListItemDto>()
            .Map(dst => dst.CustomerName, src => src.DisplayName)
            .Map(dst => dst.ContactPerson, src => GetContactPerson(src.CustomerContacts))
            .Map(dst => dst.PhoneNumber, src => GetPhoneNumber(src.CustomerContacts))
            .Map(dst => dst.Email, src => GetEmail(src.CustomerContacts))
            .Map(dst => dst.City, src => GetCity(src.CustomerAddresses))
            .Map(dst => dst.State, src => GetState(src.CustomerAddresses))
            .Map(dst => dst.Country, src => GetCountry(src.CustomerAddresses))
            .Map(dst => dst.CustomerType, src => src.CustomerType.ToString())
            .Map(dst => dst.CreditLimit, src => src.CustomerFinancial != null ? src.CustomerFinancial.CreditLimit : 0)
            .Map(dst => dst.CurrencyName, src => src.Currency != null ? src.Currency.Name : string.Empty);

        config.ForType<Customer, CustomerDto>()
            .Map(dst => dst.CurrencyName, src => src.Currency != null ? src.Currency.Name : string.Empty)
            .Map(dst => dst.CurrencySymbol, src => src.Currency != null ? src.Currency.Symbol : string.Empty)
            .Map(dst => dst.Financial, src => src.CustomerFinancial)
            .Map(dst => dst.ContactPersons, src => src.CustomerContacts.Where(cc => !cc.IsPrimary))
            .Map(dst => dst.Addresses, src => src.CustomerAddresses)
            .Map(dst => dst.TaxInfos, src => src.CustomerTaxInfos)
            .Map(dst => dst.PrimaryContact, src => GetPrimaryContact(src.CustomerContacts))
            .Map(dst => dst.BillingAddress, src => GetBillingAddress(src.CustomerAddresses))
            .Map(dst => dst.ShippingAddress, src => GetShippingAddress(src.CustomerAddresses))
            .Map(dst => dst.TaxNumber, src => GetTaxNumber(src.CustomerTaxInfos, TaxType.TaxIdentificationNumber))
            .Map(dst => dst.GSTNumber, src => GetTaxNumber(src.CustomerTaxInfos, TaxType.GST))
            .Map(dst => dst.PANNumber, src => GetTaxNumber(src.CustomerTaxInfos, TaxType.PAN));

        config.ForType<CustomerContact, CustomerContactDto>();

        config.ForType<CustomerAddress, CustomerAddressDto>();

        config.ForType<CustomerTaxInfo, CustomerTaxInfoDto>();

        config.ForType<CustomerFinancial, CustomerFinancialDto>();
    }

    #region Private Helper Methods

    private static string GetContactPerson(ICollection<CustomerContact> contacts)
    {
        var contact = contacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive);
        return contact != null ? $"{contact.FirstName} {contact.LastName}".Trim() : string.Empty;
    }

    private static string GetPhoneNumber(ICollection<CustomerContact> contacts)
    {
        return contacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.PhoneNumber ?? string.Empty;
    }

    private static string GetEmail(ICollection<CustomerContact> contacts)
    {
        return contacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.Email ?? string.Empty;
    }

    private static string GetCity(ICollection<CustomerAddress> addresses)
    {
        return addresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.City ?? string.Empty;
    }

    private static string GetState(ICollection<CustomerAddress> addresses)
    {
        return addresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.State?.Name ?? string.Empty;
    }

    private static string GetCountry(ICollection<CustomerAddress> addresses)
    {
        return addresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.Country?.Name ?? string.Empty;
    }

    // Derived Property Helper Methods
    private static CustomerContact GetPrimaryContact(ICollection<CustomerContact> contacts)
    {
        return contacts?.FirstOrDefault(c => c.IsPrimary && c.IsActive);
    }

    private static CustomerAddress GetBillingAddress(ICollection<CustomerAddress> addresses)
    {
        return addresses?.FirstOrDefault(a => a.AddressType == AddressType.Billing && a.IsActive);
    }

    private static CustomerAddress GetShippingAddress(ICollection<CustomerAddress> addresses)
    {
        return addresses?.FirstOrDefault(a => a.AddressType == AddressType.Shipping && a.IsActive);
    }

    private static string GetTaxNumber(ICollection<CustomerTaxInfo> taxInfos, TaxType taxType)
    {
        return taxInfos?.FirstOrDefault(t => t.TaxType == taxType && t.IsActive)?.TaxNumber;
    }

    #endregion
}