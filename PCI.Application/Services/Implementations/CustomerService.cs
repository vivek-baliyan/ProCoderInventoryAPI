using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Services.Implementations;

public class CustomerService(IUnitOfWork unitOfWork) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<bool>> CreateCustomer(
        string userId,
        int organisationId,
        CreateCustomerDto createCustomerDto)
    {
        var existingCustomer = await _unitOfWork.Repository<Customer>()
            .GetFirstOrDefaultAsync(x => x.CustomerCode == createCustomerDto.CustomerCode && x.OrganisationId == organisationId);

        if (existingCustomer != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerAlreadyExists, "Customer with this code already exists"));
        }

        // Check if email already exists for this organization
        if (!string.IsNullOrEmpty(createCustomerDto.Email))
        {
            var existingEmail = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.Email == createCustomerDto.Email &&
                                            x.EntityType == "Customer" &&
                                            x.OrganisationId == organisationId);
            if (existingEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
            }
        }

        try
        {
            var customer = new Customer
            {
                CustomerCode = createCustomerDto.CustomerCode,
                CustomerName = createCustomerDto.CustomerName,
                CompanyName = createCustomerDto.CompanyName,
                WebsiteUrl = createCustomerDto.WebsiteUrl,
                CustomerType = createCustomerDto.CustomerType,
                CurrencyId = createCustomerDto.CurrencyId,
                IsActive = createCustomerDto.IsActive,
                Notes = createCustomerDto.Notes,
                OrganisationId = organisationId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            _unitOfWork.Repository<Customer>().Add(customer);
            await _unitOfWork.SaveChangesAsync();

            // Create BusinessContact if contact info provided
            if (!string.IsNullOrEmpty(createCustomerDto.ContactPerson) ||
                !string.IsNullOrEmpty(createCustomerDto.Email) ||
                !string.IsNullOrEmpty(createCustomerDto.PhoneNumber) ||
                !string.IsNullOrEmpty(createCustomerDto.MobileNumber))
            {
                var businessContact = new BusinessContact
                {
                    EntityType = "Customer",
                    EntityId = customer.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = createCustomerDto.ContactPerson ?? "N/A",
                    Email = createCustomerDto.Email,
                    PhoneNumber = createCustomerDto.PhoneNumber,
                    MobileNumber = createCustomerDto.MobileNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessContact>().Add(businessContact);
            }

            // Create BusinessAddress if address info provided
            if (!string.IsNullOrEmpty(createCustomerDto.BillingAddress) ||
                !string.IsNullOrEmpty(createCustomerDto.City) ||
                !string.IsNullOrEmpty(createCustomerDto.State) ||
                !string.IsNullOrEmpty(createCustomerDto.Country))
            {
                var billingAddress = new BusinessAddress
                {
                    EntityType = "Customer",
                    EntityId = customer.Id,
                    AddressType = AddressType.Billing,
                    AddressLine1 = createCustomerDto.BillingAddress ?? "N/A",
                    City = createCustomerDto.City,
                    State = createCustomerDto.State,
                    PostalCode = createCustomerDto.PostalCode,
                    Country = createCustomerDto.Country,
                    IsDefault = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessAddress>().Add(billingAddress);

                // Create shipping address if different from billing
                if (!string.IsNullOrEmpty(createCustomerDto.ShippingAddress) &&
                    createCustomerDto.ShippingAddress != createCustomerDto.BillingAddress)
                {
                    var shippingAddress = new BusinessAddress
                    {
                        EntityType = "Customer",
                        EntityId = customer.Id,
                        AddressType = AddressType.Shipping,
                        AddressLine1 = createCustomerDto.ShippingAddress,
                        City = createCustomerDto.City,
                        State = createCustomerDto.State,
                        PostalCode = createCustomerDto.PostalCode,
                        Country = createCustomerDto.Country,
                        IsDefault = false,
                        IsActive = true,
                        OrganisationId = organisationId,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _unitOfWork.Repository<BusinessAddress>().Add(shippingAddress);
                }
            }

            // Create CustomerFinancial if financial info provided
            if (createCustomerDto.PaymentTermDays > 0 || createCustomerDto.CreditLimit.HasValue)
            {
                var customerFinancial = new CustomerFinancial
                {
                    CustomerId = customer.Id,
                    PaymentTermDays = createCustomerDto.PaymentTermDays,
                    CreditLimit = createCustomerDto.CreditLimit ?? 0,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<CustomerFinancial>().Add(customerFinancial);
            }

            // Create BusinessTaxInfo if tax info provided
            var taxInfos = new List<BusinessTaxInfo>();
            if (!string.IsNullOrEmpty(createCustomerDto.TaxNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = customer.Id,
                    TaxType = TaxType.TaxIdentificationNumber,
                    TaxNumber = createCustomerDto.TaxNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createCustomerDto.GSTNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = customer.Id,
                    TaxType = TaxType.GST,
                    TaxNumber = createCustomerDto.GSTNumber,
                    IsPrimary = string.IsNullOrEmpty(createCustomerDto.TaxNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createCustomerDto.PANNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = customer.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = createCustomerDto.PANNumber,
                    IsPrimary = string.IsNullOrEmpty(createCustomerDto.TaxNumber) && string.IsNullOrEmpty(createCustomerDto.GSTNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            foreach (var taxInfo in taxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Add(taxInfo);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerCreationError, ex.Message, ex.ToString()));
        }
    }

    public async Task<ServiceResult<bool>> UpdateCustomer(
        string userId,
        int organisationId,
        UpdateCustomerDto updateCustomerDto)
    {
        var existingCustomer = await _unitOfWork.Repository<Customer>()
            .GetFirstOrDefaultAsync(x => x.Id == updateCustomerDto.Id && x.OrganisationId == organisationId);

        if (existingCustomer == null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
        }

        // Check if customer code already exists for another customer
        var duplicateCode = await _unitOfWork.Repository<Customer>()
            .GetFirstOrDefaultAsync(x => x.CustomerCode == updateCustomerDto.CustomerCode &&
                                        x.Id != updateCustomerDto.Id &&
                                        x.OrganisationId == organisationId);

        if (duplicateCode != null)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerAlreadyExists, "Customer with this code already exists"));
        }

        // Check if email already exists for another customer
        if (!string.IsNullOrEmpty(updateCustomerDto.Email))
        {
            var duplicateEmail = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.Email == updateCustomerDto.Email &&
                                            x.EntityType == "Customer" &&
                                            x.EntityId != updateCustomerDto.Id &&
                                            x.OrganisationId == organisationId);

            if (duplicateEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
            }
        }

        try
        {
            // Update core customer fields
            existingCustomer.CustomerCode = updateCustomerDto.CustomerCode;
            existingCustomer.CustomerName = updateCustomerDto.CustomerName;
            existingCustomer.CompanyName = updateCustomerDto.CompanyName;
            existingCustomer.WebsiteUrl = updateCustomerDto.WebsiteUrl;
            existingCustomer.CustomerType = updateCustomerDto.CustomerType;
            existingCustomer.CurrencyId = updateCustomerDto.CurrencyId;
            existingCustomer.IsActive = updateCustomerDto.IsActive;
            existingCustomer.Notes = updateCustomerDto.Notes;
            existingCustomer.ModifiedBy = userId;
            existingCustomer.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Repository<Customer>().Update(existingCustomer);

            // Update or create BusinessContact
            var existingContact = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Customer" &&
                                           x.EntityId == updateCustomerDto.Id &&
                                           x.IsPrimary == true);

            if (existingContact != null)
            {
                existingContact.ContactPersonName = updateCustomerDto.ContactPerson ?? "N/A";
                existingContact.Email = updateCustomerDto.Email;
                existingContact.PhoneNumber = updateCustomerDto.PhoneNumber;
                existingContact.MobileNumber = updateCustomerDto.MobileNumber;
                existingContact.ModifiedBy = userId;
                existingContact.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<BusinessContact>().Update(existingContact);
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.ContactPerson) ||
                     !string.IsNullOrEmpty(updateCustomerDto.Email) ||
                     !string.IsNullOrEmpty(updateCustomerDto.PhoneNumber) ||
                     !string.IsNullOrEmpty(updateCustomerDto.MobileNumber))
            {
                var newContact = new BusinessContact
                {
                    EntityType = "Customer",
                    EntityId = updateCustomerDto.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = updateCustomerDto.ContactPerson ?? "N/A",
                    Email = updateCustomerDto.Email,
                    PhoneNumber = updateCustomerDto.PhoneNumber,
                    MobileNumber = updateCustomerDto.MobileNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessContact>().Add(newContact);
            }

            // Update or create BusinessAddress (Billing)
            var existingBillingAddress = await _unitOfWork.Repository<BusinessAddress>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Customer" &&
                                           x.EntityId == updateCustomerDto.Id &&
                                           x.AddressType == AddressType.Billing);

            if (existingBillingAddress != null)
            {
                existingBillingAddress.AddressLine1 = updateCustomerDto.BillingAddress ?? "N/A";
                existingBillingAddress.City = updateCustomerDto.City;
                existingBillingAddress.State = updateCustomerDto.State;
                existingBillingAddress.PostalCode = updateCustomerDto.PostalCode;
                existingBillingAddress.Country = updateCustomerDto.Country;
                existingBillingAddress.ModifiedBy = userId;
                existingBillingAddress.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<BusinessAddress>().Update(existingBillingAddress);
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.BillingAddress) ||
                     !string.IsNullOrEmpty(updateCustomerDto.City) ||
                     !string.IsNullOrEmpty(updateCustomerDto.State) ||
                     !string.IsNullOrEmpty(updateCustomerDto.Country))
            {
                var newBillingAddress = new BusinessAddress
                {
                    EntityType = "Customer",
                    EntityId = updateCustomerDto.Id,
                    AddressType = AddressType.Billing,
                    AddressLine1 = updateCustomerDto.BillingAddress ?? "N/A",
                    City = updateCustomerDto.City,
                    State = updateCustomerDto.State,
                    PostalCode = updateCustomerDto.PostalCode,
                    Country = updateCustomerDto.Country,
                    IsDefault = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessAddress>().Add(newBillingAddress);
            }

            // Update or create BusinessAddress (Shipping)
            var existingShippingAddress = await _unitOfWork.Repository<BusinessAddress>()
                .GetFirstOrDefaultAsync(x => x.EntityType == "Customer" &&
                                           x.EntityId == updateCustomerDto.Id &&
                                           x.AddressType == AddressType.Shipping);

            if (!string.IsNullOrEmpty(updateCustomerDto.ShippingAddress) &&
                updateCustomerDto.ShippingAddress != updateCustomerDto.BillingAddress)
            {
                if (existingShippingAddress != null)
                {
                    existingShippingAddress.AddressLine1 = updateCustomerDto.ShippingAddress;
                    existingShippingAddress.City = updateCustomerDto.City;
                    existingShippingAddress.State = updateCustomerDto.State;
                    existingShippingAddress.PostalCode = updateCustomerDto.PostalCode;
                    existingShippingAddress.Country = updateCustomerDto.Country;
                    existingShippingAddress.ModifiedBy = userId;
                    existingShippingAddress.ModifiedOn = DateTime.UtcNow;
                    _unitOfWork.Repository<BusinessAddress>().Update(existingShippingAddress);
                }
                else
                {
                    var newShippingAddress = new BusinessAddress
                    {
                        EntityType = "Customer",
                        EntityId = updateCustomerDto.Id,
                        AddressType = AddressType.Shipping,
                        AddressLine1 = updateCustomerDto.ShippingAddress,
                        City = updateCustomerDto.City,
                        State = updateCustomerDto.State,
                        PostalCode = updateCustomerDto.PostalCode,
                        Country = updateCustomerDto.Country,
                        IsDefault = false,
                        IsActive = true,
                        OrganisationId = organisationId,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _unitOfWork.Repository<BusinessAddress>().Add(newShippingAddress);
                }
            }
            else if (existingShippingAddress != null)
            {
                // Remove shipping address if it's the same as billing or empty
                _unitOfWork.Repository<BusinessAddress>().Remove(existingShippingAddress);
            }

            // Update or create CustomerFinancial
            var existingFinancial = await _unitOfWork.Repository<CustomerFinancial>()
                .GetFirstOrDefaultAsync(x => x.CustomerId == updateCustomerDto.Id);

            if (existingFinancial != null)
            {
                existingFinancial.PaymentTermDays = updateCustomerDto.PaymentTermDays;
                existingFinancial.CreditLimit = updateCustomerDto.CreditLimit ?? 0;
                existingFinancial.ModifiedBy = userId;
                existingFinancial.ModifiedOn = DateTime.UtcNow;
                _unitOfWork.Repository<CustomerFinancial>().Update(existingFinancial);
            }
            else if (updateCustomerDto.PaymentTermDays > 0 || updateCustomerDto.CreditLimit.HasValue)
            {
                var newFinancial = new CustomerFinancial
                {
                    CustomerId = updateCustomerDto.Id,
                    PaymentTermDays = updateCustomerDto.PaymentTermDays,
                    CreditLimit = updateCustomerDto.CreditLimit ?? 0,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<CustomerFinancial>().Add(newFinancial);
            }

            // Update BusinessTaxInfo
            var existingTaxInfos = await _unitOfWork.Repository<BusinessTaxInfo>()
                .GetFilteredAsync(x => x.EntityType == "Customer" && x.EntityId == updateCustomerDto.Id);

            // Remove existing tax info
            foreach (var taxInfo in existingTaxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Remove(taxInfo);
            }

            // Add new tax info
            var newTaxInfos = new List<BusinessTaxInfo>();
            if (!string.IsNullOrEmpty(updateCustomerDto.TaxNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = updateCustomerDto.Id,
                    TaxType = TaxType.TaxIdentificationNumber,
                    TaxNumber = updateCustomerDto.TaxNumber,
                    IsPrimary = true,
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(updateCustomerDto.GSTNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = updateCustomerDto.Id,
                    TaxType = TaxType.GST,
                    TaxNumber = updateCustomerDto.GSTNumber,
                    IsPrimary = string.IsNullOrEmpty(updateCustomerDto.TaxNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(updateCustomerDto.PANNumber))
            {
                newTaxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = "Customer",
                    EntityId = updateCustomerDto.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = updateCustomerDto.PANNumber,
                    IsPrimary = string.IsNullOrEmpty(updateCustomerDto.TaxNumber) && string.IsNullOrEmpty(updateCustomerDto.GSTNumber),
                    IsActive = true,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            foreach (var taxInfo in newTaxInfos)
            {
                _unitOfWork.Repository<BusinessTaxInfo>().Add(taxInfo);
            }

            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerUpdateError, ex.Message));
        }
    }

    public async Task<ServiceResult<CustomerDto>> GetCustomerById(int id)
    {
        try
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    includeroperties: "Currency,BusinessContacts,BusinessAddresses,BusinessTaxInfos,CustomerFinancial");

            if (customer == null)
            {
                return ServiceResult<CustomerDto>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.CurrencyName = customer.Currency?.Name;
            customerDto.CurrencySymbol = customer.Currency?.Symbol;

            // Get primary contact info
            var primaryContact = customer.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive);
            if (primaryContact != null)
            {
                customerDto.ContactPerson = primaryContact.ContactPersonName;
                customerDto.Email = primaryContact.Email;
                customerDto.PhoneNumber = primaryContact.PhoneNumber;
                customerDto.MobileNumber = primaryContact.MobileNumber;
            }

            // Get billing address
            var billingAddress = customer.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive);
            if (billingAddress != null)
            {
                customerDto.BillingAddress = billingAddress.AddressLine1;
                customerDto.City = billingAddress.City;
                customerDto.State = billingAddress.State;
                customerDto.PostalCode = billingAddress.PostalCode;
                customerDto.Country = billingAddress.Country;
            }

            // Get shipping address
            var shippingAddress = customer.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Shipping && x.IsActive);
            if (shippingAddress != null)
            {
                customerDto.ShippingAddress = shippingAddress.AddressLine1;
            }

            // Get financial info
            if (customer.CustomerFinancial != null)
            {
                customerDto.PaymentTermDays = customer.CustomerFinancial.PaymentTermDays;
                customerDto.CreditLimit = customer.CustomerFinancial.CreditLimit;
            }

            // Get tax info
            var taxNumber = customer.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.TaxIdentificationNumber && x.IsActive);
            if (taxNumber != null)
            {
                customerDto.TaxNumber = taxNumber.TaxNumber;
            }

            var gstNumber = customer.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.GST && x.IsActive);
            if (gstNumber != null)
            {
                customerDto.GSTNumber = gstNumber.TaxNumber;
            }

            var panNumber = customer.BusinessTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.PAN && x.IsActive);
            if (panNumber != null)
            {
                customerDto.PANNumber = panNumber.TaxNumber;
            }

            return ServiceResult<CustomerDto>.Success(customerDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<CustomerDto>
                .Error(new Problem(ErrorCodes.CustomerRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<CustomerListItemDto>>> GetAllCustomers(int organisationId, int pageIndex, int pageSize)
    {
        try
        {
            var customers = await _unitOfWork.Repository<Customer>()
                .GetPaginatedAsync(
                    pageIndex,
                    pageSize,
                    filter: x => x.OrganisationId == organisationId,
                    includeroperties: "Currency,BusinessContacts,BusinessAddresses,CustomerFinancial");

            var customerDtos = customers.Select(c => new CustomerListItemDto
            {
                Id = c.Id,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CompanyName = c.CompanyName,
                ContactPerson = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.ContactPersonName,
                PhoneNumber = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.PhoneNumber,
                Email = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.Email,
                City = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.City,
                State = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.State,
                Country = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.Country,
                CustomerType = c.CustomerType,
                CreditLimit = c.CustomerFinancial?.CreditLimit,
                IsActive = c.IsActive,
                CurrencyName = c.Currency?.Name,
                CreatedOn = c.CreatedOn
            }).ToList();

            return ServiceResult<List<CustomerListItemDto>>.Success(customerDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<CustomerListItemDto>>
                .Error(new Problem(ErrorCodes.CustomerRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<List<CustomerListItemDto>>> GetFilteredCustomers(int organisationId, CustomerFilterDto filter)
    {
        try
        {
            var specification = new CustomerSpecification(organisationId, filter);
            var customers = await _unitOfWork.Repository<Customer>().GetAsync(specification);

            var customerDtos = customers.Select(c => new CustomerListItemDto
            {
                Id = c.Id,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CompanyName = c.CompanyName,
                ContactPerson = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.ContactPersonName,
                PhoneNumber = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.PhoneNumber,
                Email = c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.Email,
                City = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.City,
                State = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.State,
                Country = c.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive)?.Country,
                CustomerType = c.CustomerType,
                CreditLimit = c.CustomerFinancial?.CreditLimit,
                IsActive = c.IsActive,
                CurrencyName = c.Currency?.Name,
                CreatedOn = c.CreatedOn
            }).ToList();

            return ServiceResult<List<CustomerListItemDto>>.Success(customerDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<CustomerListItemDto>>
                .Error(new Problem(ErrorCodes.CustomerRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> DeleteCustomer(int id, string userId)
    {
        try
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            // Check if customer has any related transactions
            var hasSalesOrders = await _unitOfWork.Repository<SalesOrder>()
                .AnyAsync(x => x.CustomerId == id);

            var hasInvoices = await _unitOfWork.Repository<Invoice>()
                .AnyAsync(x => x.CustomerId == id);

            if (hasSalesOrders || hasInvoices)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerHasTransactions, "Cannot delete customer with existing transactions"));
            }

            _unitOfWork.Repository<Customer>().Remove(customer);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerDeletionError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> ToggleCustomerStatus(int id, string userId)
    {
        try
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            customer.IsActive = !customer.IsActive;
            customer.ModifiedBy = userId;
            customer.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Repository<Customer>().Update(customer);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerUpdateError, ex.Message));
        }
    }
}