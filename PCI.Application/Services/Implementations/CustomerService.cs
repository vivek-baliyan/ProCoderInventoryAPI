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

public class CustomerService(IUnitOfWork unitOfWork, ICodeGenerationService codeGenerationService) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICodeGenerationService _codeGenerationService = codeGenerationService;

    public async Task<ServiceResult<bool>> CreateCustomer(
        string userId,
        int organisationId,
        CreateCustomerDto createCustomerDto)
    {
        // Generate unique customer code
        var customerCode = await _codeGenerationService.GenerateCustomerCodeAsync(organisationId);

        // Check if email already exists for this organization
        if (!string.IsNullOrEmpty(createCustomerDto.Email))
        {
            var existingEmail = await _unitOfWork.Repository<BusinessContact>()
                .GetFirstOrDefaultAsync(x => x.Email == createCustomerDto.Email &&
                                            x.EntityType == EntityTypes.Customer &&
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
                CustomerCode = customerCode,
                CustomerName = createCustomerDto.CustomerName,
                CompanyName = createCustomerDto.CompanyName,
                WebsiteUrl = createCustomerDto.WebsiteUrl,
                CustomerType = (CustomerType)createCustomerDto.CustomerType,
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
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = createCustomerDto.ContactPerson ?? DefaultValues.NotAvailable,
                    Email = createCustomerDto.Email,
                    PhoneNumber = createCustomerDto.PhoneNumber,
                    MobileNumber = createCustomerDto.MobileNumber,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
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
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    AddressType = AddressType.Billing,
                    AddressLine1 = createCustomerDto.BillingAddress ?? DefaultValues.NotAvailable,
                    City = createCustomerDto.City,
                    State = createCustomerDto.State,
                    PostalCode = createCustomerDto.PostalCode,
                    Country = createCustomerDto.Country,
                    IsDefault = DefaultValues.DefaultIsDefault,
                    IsActive = DefaultValues.DefaultIsActive,
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
                        EntityType = EntityTypes.Customer,
                        EntityId = customer.Id,
                        AddressType = AddressType.Shipping,
                        AddressLine1 = createCustomerDto.ShippingAddress,
                        City = createCustomerDto.City,
                        State = createCustomerDto.State,
                        PostalCode = createCustomerDto.PostalCode,
                        Country = createCustomerDto.Country,
                        IsDefault = false,
                        IsActive = DefaultValues.DefaultIsActive,
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
                    CreditLimit = createCustomerDto.CreditLimit ?? DefaultValues.DefaultCreditLimit,
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
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    TaxType = TaxType.TaxIdentificationNumber,
                    TaxNumber = createCustomerDto.TaxNumber,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createCustomerDto.GSTNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    TaxType = TaxType.GST,
                    TaxNumber = createCustomerDto.GSTNumber,
                    IsPrimary = string.IsNullOrEmpty(createCustomerDto.TaxNumber),
                    IsActive = DefaultValues.DefaultIsActive,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            if (!string.IsNullOrEmpty(createCustomerDto.PANNumber))
            {
                taxInfos.Add(new BusinessTaxInfo
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = createCustomerDto.PANNumber,
                    IsPrimary = string.IsNullOrEmpty(createCustomerDto.TaxNumber) && string.IsNullOrEmpty(createCustomerDto.GSTNumber),
                    IsActive = DefaultValues.DefaultIsActive,
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
        try
        {
            var customer = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(
                    x => x.Id == updateCustomerDto.Id && x.OrganisationId == organisationId,
                    "BusinessContacts,BusinessAddresses,BusinessTaxInfos,CustomerFinancial");

            if (customer == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            // Concurrency check
            if (!customer.RowVersion.SequenceEqual(updateCustomerDto.RowVersion))
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.ConcurrencyError, "Customer has been modified by another user"));
            }

            // Validate unique email
            if (!string.IsNullOrEmpty(updateCustomerDto.Email))
            {
                var duplicateEmail = await _unitOfWork.Repository<BusinessContact>()
                    .GetFirstOrDefaultAsync(x => x.Email == updateCustomerDto.Email &&
                                                x.EntityType == EntityTypes.Customer &&
                                                x.EntityId != updateCustomerDto.Id &&
                                                x.OrganisationId == organisationId);

                if (duplicateEmail != null)
                {
                    return ServiceResult<bool>
                        .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
                }
            }

            // Update customer properties using Mapster
            updateCustomerDto.Adapt(customer);
            customer.ModifiedBy = userId;
            customer.ModifiedOn = DateTime.UtcNow;

            // Update primary contact
            var primaryContact = customer.BusinessContacts?.FirstOrDefault(x => x.IsPrimary);
            if (primaryContact != null)
            {
                primaryContact.ContactPersonName = updateCustomerDto.ContactPerson ?? DefaultValues.NotAvailable;
                primaryContact.Email = updateCustomerDto.Email;
                primaryContact.PhoneNumber = updateCustomerDto.PhoneNumber;
                primaryContact.MobileNumber = updateCustomerDto.MobileNumber;
                primaryContact.ModifiedBy = userId;
                primaryContact.ModifiedOn = DateTime.UtcNow;
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.ContactPerson) || !string.IsNullOrEmpty(updateCustomerDto.Email))
            {
                customer.BusinessContacts ??= new List<BusinessContact>();
                customer.BusinessContacts.Add(new BusinessContact
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    ContactType = ContactType.Primary,
                    ContactPersonName = updateCustomerDto.ContactPerson ?? DefaultValues.NotAvailable,
                    Email = updateCustomerDto.Email,
                    PhoneNumber = updateCustomerDto.PhoneNumber,
                    MobileNumber = updateCustomerDto.MobileNumber,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            // Update billing address
            var billingAddress = customer.BusinessAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing);
            if (billingAddress != null)
            {
                billingAddress.AddressLine1 = updateCustomerDto.BillingAddress ?? DefaultValues.NotAvailable;
                billingAddress.City = updateCustomerDto.City;
                billingAddress.State = updateCustomerDto.State;
                billingAddress.PostalCode = updateCustomerDto.PostalCode;
                billingAddress.Country = updateCustomerDto.Country;
                billingAddress.ModifiedBy = userId;
                billingAddress.ModifiedOn = DateTime.UtcNow;
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.BillingAddress))
            {
                customer.BusinessAddresses ??= new List<BusinessAddress>();
                customer.BusinessAddresses.Add(new BusinessAddress
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    AddressType = AddressType.Billing,
                    AddressLine1 = updateCustomerDto.BillingAddress,
                    City = updateCustomerDto.City,
                    State = updateCustomerDto.State,
                    PostalCode = updateCustomerDto.PostalCode,
                    Country = updateCustomerDto.Country,
                    IsDefault = DefaultValues.DefaultIsDefault,
                    IsActive = DefaultValues.DefaultIsActive,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            // Update financial info
            if (customer.CustomerFinancial != null)
            {
                customer.CustomerFinancial.PaymentTermDays = updateCustomerDto.PaymentTermDays;
                customer.CustomerFinancial.CreditLimit = updateCustomerDto.CreditLimit ?? DefaultValues.DefaultCreditLimit;
                customer.CustomerFinancial.ModifiedBy = userId;
                customer.CustomerFinancial.ModifiedOn = DateTime.UtcNow;
            }
            else if (updateCustomerDto.PaymentTermDays > 0 || updateCustomerDto.CreditLimit.HasValue)
            {
                customer.CustomerFinancial = new CustomerFinancial
                {
                    CustomerId = customer.Id,
                    PaymentTermDays = updateCustomerDto.PaymentTermDays,
                    CreditLimit = updateCustomerDto.CreditLimit ?? DefaultValues.DefaultCreditLimit,
                    OrganisationId = organisationId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
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