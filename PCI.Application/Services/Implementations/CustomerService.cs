using Mapster;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
using PCI.Shared.Common.Enums;
using PCI.Shared.Dtos.Common;
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
            var existingEmail = await _unitOfWork.Repository<CustomerContact>()
                .GetFirstOrDefaultAsync(x => x.Email == createCustomerDto.Email);
            if (existingEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
            }
        }

        try
        {
            var customer = createCustomerDto.Adapt<Customer>();
            customer.CustomerCode = customerCode;
            customer.OrganisationId = organisationId;
            customer.CreatedBy = userId;
            customer.CreatedOn = DateTime.UtcNow;


            _unitOfWork.Repository<Customer>().Add(customer);
            await _unitOfWork.SaveChangesAsync();

            // Create CustomerContact if contact info provided
            if (!string.IsNullOrEmpty(createCustomerDto.FirstName) ||
                !string.IsNullOrEmpty(createCustomerDto.LastName) ||
                !string.IsNullOrEmpty(createCustomerDto.Email) ||
                !string.IsNullOrEmpty(createCustomerDto.WorkPhone) ||
                !string.IsNullOrEmpty(createCustomerDto.Mobile))
            {
                var customerContact = new CustomerContact
                {
                    CustomerId = customer.Id,
                    ContactType = ContactType.Primary,
                    Salutation = createCustomerDto.Salutation,
                    FirstName = createCustomerDto.FirstName,
                    LastName = createCustomerDto.LastName,
                    Email = createCustomerDto.Email,
                    PhoneNumber = createCustomerDto.WorkPhone,
                    MobileNumber = createCustomerDto.Mobile,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<CustomerContact>().Add(customerContact);
            }

            // Note: Address creation will be handled separately via CustomerAddress endpoints
            // Core customer form only contains basic customer information

            // Create default CustomerFinancial record
            var customerFinancial = new CustomerFinancial
            {
                CustomerId = customer.Id,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            _unitOfWork.Repository<CustomerFinancial>().Add(customerFinancial);

            // Create CustomerTaxInfo for PAN if provided
            if (!string.IsNullOrEmpty(createCustomerDto.PAN))
            {
                var panTaxInfo = new CustomerTaxInfo
                {
                    CustomerId = customer.Id,
                    TaxType = TaxType.PAN,
                    TaxNumber = createCustomerDto.PAN,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                _unitOfWork.Repository<CustomerTaxInfo>().Add(panTaxInfo);
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
                    "CustomerContacts,CustomerAddresses,CustomerTaxInfos,CustomerFinancial");

            if (customer == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            // Removed concurrency check as RowVersion is no longer used

            // Validate unique email
            if (!string.IsNullOrEmpty(updateCustomerDto.Email))
            {
                var duplicateEmail = await _unitOfWork.Repository<CustomerContact>()
                    .GetFirstOrDefaultAsync(x => x.Email == updateCustomerDto.Email &&
                                                x.CustomerId != updateCustomerDto.Id);

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
            var primaryContact = customer.CustomerContacts?.FirstOrDefault(x => x.IsPrimary);
            if (primaryContact != null)
            {
                primaryContact.Salutation = updateCustomerDto.Salutation;
                primaryContact.FirstName = updateCustomerDto.FirstName;
                primaryContact.LastName = updateCustomerDto.LastName;
                primaryContact.Email = updateCustomerDto.Email;
                primaryContact.PhoneNumber = updateCustomerDto.WorkPhone;
                primaryContact.MobileNumber = updateCustomerDto.Mobile;
                primaryContact.ModifiedBy = userId;
                primaryContact.ModifiedOn = DateTime.UtcNow;
            }
            else if (!string.IsNullOrEmpty(updateCustomerDto.FirstName) || !string.IsNullOrEmpty(updateCustomerDto.LastName) || !string.IsNullOrEmpty(updateCustomerDto.Email))
            {
                customer.CustomerContacts ??= new List<CustomerContact>();
                customer.CustomerContacts.Add(new CustomerContact
                {
                    CustomerId = customer.Id,
                    ContactType = ContactType.Primary,
                    Salutation = updateCustomerDto.Salutation,
                    FirstName = updateCustomerDto.FirstName,
                    LastName = updateCustomerDto.LastName,
                    Email = updateCustomerDto.Email,
                    PhoneNumber = updateCustomerDto.WorkPhone,
                    MobileNumber = updateCustomerDto.Mobile,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            // Note: Address updates will be handled separately via CustomerAddress endpoints

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
                    includeroperties: "Currency,CustomerContacts,CustomerAddresses,CustomerTaxInfos,CustomerFinancial");

            if (customer == null)
            {
                return ServiceResult<CustomerDto>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.CurrencyName = customer.Currency?.Name;
            customerDto.CurrencySymbol = customer.Currency?.Symbol;

            // Get primary contact info
            var primaryContact = customer.CustomerContacts?.FirstOrDefault(x => x.IsPrimary && x.IsActive);
            if (primaryContact != null)
            {
                customerDto.Salutation = primaryContact.Salutation;
                customerDto.FirstName = primaryContact.FirstName;
                customerDto.LastName = primaryContact.LastName;
                customerDto.Email = primaryContact.Email;
                customerDto.ContactPerson = $"{primaryContact.FirstName} {primaryContact.LastName}".Trim();
                customerDto.PhoneNumber = primaryContact.PhoneNumber;
                customerDto.MobileNumber = primaryContact.MobileNumber;
            }

            // Get billing address
            var billingAddress = customer.CustomerAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Billing && x.IsActive);
            if (billingAddress != null)
            {
                customerDto.BillingAddress = billingAddress.AddressLine1;
                customerDto.City = billingAddress.City;
                customerDto.State = billingAddress.State;
                customerDto.PostalCode = billingAddress.PostalCode;
                customerDto.Country = billingAddress.Country;
            }

            // Get shipping address
            var shippingAddress = customer.CustomerAddresses?.FirstOrDefault(x => x.AddressType == AddressType.Shipping && x.IsActive);
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
            var taxNumber = customer.CustomerTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.TaxIdentificationNumber && x.IsActive);
            if (taxNumber != null)
            {
                customerDto.TaxNumber = taxNumber.TaxNumber;
            }

            var gstNumber = customer.CustomerTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.GST && x.IsActive);
            if (gstNumber != null)
            {
                customerDto.GSTNumber = gstNumber.TaxNumber;
            }

            var panNumber = customer.CustomerTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.PAN && x.IsActive);
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
                    includeroperties: "Currency,CustomerContacts,CustomerAddresses,CustomerFinancial");

            var customerDtos = customers.Adapt<List<CustomerListItemDto>>();

            return ServiceResult<List<CustomerListItemDto>>.Success(customerDtos);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<CustomerListItemDto>>
                .Error(new Problem(ErrorCodes.CustomerRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<PaginatedResult<CustomerListItemDto>>> GetFilteredCustomers(int organisationId, CustomerFilterDto filter)
    {
        try
        {
            var specification = new CustomerSpecification(organisationId, filter);
            var customers = await _unitOfWork.Repository<Customer>().GetAsync(specification);

            // Get total count without pagination
            var countSpecification = new CustomerSpecification(organisationId, filter);
            countSpecification.ClearPaging();
            var totalCount = await _unitOfWork.Repository<Customer>().CountAsync(countSpecification);

            var customerDtos = customers.Adapt<List<CustomerListItemDto>>();

            var result = new PaginatedResult<CustomerListItemDto>
            {
                Data = customerDtos,
                TotalCount = totalCount,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize
            };

            return ServiceResult<PaginatedResult<CustomerListItemDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<PaginatedResult<CustomerListItemDto>>
                .Error(new Problem(ErrorCodes.CustomerRetrievalError, ex.Message));
        }
    }

    public async Task<ServiceResult<bool>> DeleteCustomer(int id, string userId)
    {
        try
        {
            // Check if customer exists
            var customer = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            // Check for existing business transactions (prevent deletion if exists)
            var hasSalesOrders = await _unitOfWork.Repository<SalesOrder>()
                .AnyAsync(x => x.CustomerId == id);

            var hasInvoices = await _unitOfWork.Repository<Invoice>()
                .AnyAsync(x => x.CustomerId == id);

            if (hasSalesOrders || hasInvoices)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerDeletionError,
                        "Cannot delete customer with existing sales orders or invoices. Please archive the customer instead."));
            }

            _unitOfWork.Repository<Customer>().Remove(customer);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>
                .Error(new Problem(ErrorCodes.CustomerDeletionError, ex.Message, ex.ToString()));
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