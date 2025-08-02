using Mapster;
using PCI.Domain.Models;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos;
using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Mappings;

public static class ApplicationMapsterMappings
{
    public static void Configure()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        config.ForType<Organisation, OrganisationDto>()
              .Map(dst => dst.OrganisationId, src => src.Id);

        config.ForType<CreateCustomerDto, Customer>()
            .Map(dst => dst.CustomerType, src => (CustomerType)src.CustomerType);

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
    }

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
}
