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
                                            x.EntityType == EntityTypes.Customer);
            if (existingEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
            }
        }

        try
        {

            var customer = createCustomerDto.Adapt<Customer>();
            //var customer = new Customer
            //{
            //    CustomerCode = customerCode,
            //    CustomerName = createCustomerDto.CustomerName,
            //    CompanyName = createCustomerDto.CompanyName,
            //    WebsiteUrl = createCustomerDto.WebsiteUrl,
            //    CustomerType = (CustomerType)createCustomerDto.CustomerType,
            //    CurrencyId = createCustomerDto.CurrencyId,
            //    IsActive = createCustomerDto.IsActive,
            //    Notes = createCustomerDto.Notes,
            //    OrganisationId = organisationId,
            //    CreatedBy = userId,
            //    CreatedOn = DateTime.UtcNow
            //};

            customer.CustomerCode = customerCode;

            _unitOfWork.Repository<Customer>().Add(customer);
            await _unitOfWork.SaveChangesAsync();

            // Create BusinessContact if contact info provided
            if (!string.IsNullOrEmpty(createCustomerDto.FirstName) ||
                !string.IsNullOrEmpty(createCustomerDto.LastName) ||
                !string.IsNullOrEmpty(createCustomerDto.Email) ||
                !string.IsNullOrEmpty(createCustomerDto.WorkPhone) ||
                !string.IsNullOrEmpty(createCustomerDto.Mobile))
            {
                var businessContact = new BusinessContact
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    ContactType = ContactType.Primary,
                    Salutation = createCustomerDto.Salutation,
                    FirstName = createCustomerDto.FirstName,
                    LastName = createCustomerDto.LastName,
                    // ContactPersonName computed from FirstName + LastName
                    Email = createCustomerDto.Email,
                    PhoneNumber = createCustomerDto.WorkPhone,
                    MobileNumber = createCustomerDto.Mobile,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessContact>().Add(businessContact);
            }

            // Note: Address creation will be handled separately via BusinessAddress endpoints
            // Core customer form only contains basic customer information

            // Create default CustomerFinancial record
            var customerFinancial = new CustomerFinancial
            {
                CustomerId = customer.Id,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            _unitOfWork.Repository<CustomerFinancial>().Add(customerFinancial);

            // Create BusinessTaxInfo for PAN if provided
            if (!string.IsNullOrEmpty(createCustomerDto.PAN))
            {
                var panTaxInfo = new BusinessTaxInfo
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = createCustomerDto.PAN,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<BusinessTaxInfo>().Add(panTaxInfo);
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
                                                x.EntityId != updateCustomerDto.Id);

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
                primaryContact.Salutation = updateCustomerDto.Salutation;
                primaryContact.FirstName = updateCustomerDto.FirstName;
                primaryContact.LastName = updateCustomerDto.LastName;
                // ContactPersonName computed from FirstName + LastName
                primaryContact.Email = updateCustomerDto.Email;
                primaryContact.PhoneNumber = updateCustomerDto.WorkPhone;
                primaryContact.MobileNumber = updateCustomerDto.Mobile;
                primaryContact.ModifiedBy = userId;
                primaryContact.ModifiedOn = DateTime.UtcNow;
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.FirstName) || !string.IsNullOrEmpty(updateCustomerDto.LastName) || !string.IsNullOrEmpty(updateCustomerDto.Email))
            {
                customer.BusinessContacts ??= new List<BusinessContact>();
                customer.BusinessContacts.Add(new BusinessContact
                {
                    EntityType = EntityTypes.Customer,
                    EntityId = customer.Id,
                    ContactType = ContactType.Primary,
                    Salutation = updateCustomerDto.Salutation,
                    FirstName = updateCustomerDto.FirstName,
                    LastName = updateCustomerDto.LastName,
                    // ContactPersonName computed from FirstName + LastName
                    Email = updateCustomerDto.Email,
                    PhoneNumber = updateCustomerDto.WorkPhone,
                    MobileNumber = updateCustomerDto.Mobile,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            // Note: Address updates will be handled separately via BusinessAddress endpoints

            // Note: Financial info updates will be handled via CustomerFinancial endpoints

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
                customerDto.Salutation = primaryContact.Salutation;
                customerDto.FirstName = primaryContact.FirstName;
                customerDto.LastName = primaryContact.LastName;
                customerDto.Email = primaryContact.Email;
                customerDto.WorkPhone = primaryContact.PhoneNumber;
                customerDto.Mobile = primaryContact.MobileNumber;

                // Legacy fields for backward compatibility
                customerDto.ContactPerson = $"{primaryContact.FirstName} {primaryContact.LastName}".Trim();
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
                ContactPerson = $"{c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.FirstName} {c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.LastName}".Trim(),
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
                ContactPerson = $"{c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.FirstName} {c.BusinessContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive)?.LastName}".Trim(),
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