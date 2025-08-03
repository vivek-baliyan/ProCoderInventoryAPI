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
        // Validate the request
        var validationResult = await ValidateCreateCustomerAsync(createCustomerDto);
        if (!validationResult.Succeeded)
            return validationResult;

        // Generate unique customer code
        var customerCode = await _codeGenerationService.GenerateCustomerCodeAsync(organisationId);

        using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            // Create the main customer entity
            var customer = CreateCustomerEntity(createCustomerDto, customerCode, organisationId, userId);
            _unitOfWork.Repository<Customer>().Add(customer);

            // Save customer to get the generated ID
            await _unitOfWork.SaveChangesAsync();

            // Now create related entities with proper customer ID
            var contacts = CreateCustomerContacts(customer.Id, createCustomerDto, userId);
            var addresses = CreateCustomerAddresses(customer.Id, createCustomerDto, userId);
            var financial = CreateCustomerFinancial(customer.Id, createCustomerDto, userId);
            var taxInfo = CreateCustomerTaxInfo(customer.Id, createCustomerDto, userId);

            // Add all related entities to repository
            foreach (var contact in contacts)
                _unitOfWork.Repository<CustomerContact>().Add(contact);

            foreach (var address in addresses)
                _unitOfWork.Repository<CustomerAddress>().Add(address);

            _unitOfWork.Repository<CustomerFinancial>().Add(financial);

            if (taxInfo != null)
                _unitOfWork.Repository<CustomerTaxInfo>().Add(taxInfo);

            // Save all related entities
            await _unitOfWork.SaveChangesAsync();

            // Commit the transaction
            await transaction.CommitAsync();
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
                primaryContact.PhoneNumber = updateCustomerDto.PhoneNumber;
                primaryContact.MobileNumber = updateCustomerDto.MobileNumber;
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
                    PhoneNumber = updateCustomerDto.PhoneNumber,
                    MobileNumber = updateCustomerDto.MobileNumber,
                    IsPrimary = DefaultValues.DefaultIsPrimary,
                    IsActive = DefaultValues.DefaultIsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                });
            }

            // Update financial information if provided
            if (updateCustomerDto.CreditLimit.HasValue || !string.IsNullOrEmpty(updateCustomerDto.PaymentTerms))
            {
                if (customer.CustomerFinancial == null)
                {
                    customer.CustomerFinancial = new CustomerFinancial
                    {
                        CustomerId = customer.Id,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                }

                if (updateCustomerDto.CreditLimit.HasValue)
                    customer.CustomerFinancial.CreditLimit = updateCustomerDto.CreditLimit.Value;

                customer.CustomerFinancial.ModifiedBy = userId;
                customer.CustomerFinancial.ModifiedOn = DateTime.UtcNow;
            }

            // Update tax information if PAN is provided
            if (!string.IsNullOrEmpty(updateCustomerDto.Pan))
            {
                var existingPanInfo = customer.CustomerTaxInfos?.FirstOrDefault(x => x.TaxType == TaxType.PAN);
                if (existingPanInfo != null)
                {
                    existingPanInfo.TaxNumber = updateCustomerDto.Pan;
                    existingPanInfo.ModifiedBy = userId;
                    existingPanInfo.ModifiedOn = DateTime.UtcNow;
                }
                else
                {
                    customer.CustomerTaxInfos ??= new List<CustomerTaxInfo>();
                    customer.CustomerTaxInfos.Add(new CustomerTaxInfo
                    {
                        CustomerId = customer.Id,
                        TaxType = TaxType.PAN,
                        TaxNumber = updateCustomerDto.Pan,
                        IsPrimary = DefaultValues.DefaultIsPrimary,
                        IsActive = DefaultValues.DefaultIsActive,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    });
                }
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

    #region Private Helper Methods for CreateCustomer

    private async Task<ServiceResult<bool>> ValidateCreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
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

        // Check for additional contact person emails
        if (createCustomerDto.ContactPersons?.Any() == true)
        {
            var contactEmails = createCustomerDto.ContactPersons
                .Where(cp => !string.IsNullOrEmpty(cp.Email))
                .Select(cp => cp.Email)
                .ToList();

            if (contactEmails.Any())
            {
                var existingContactEmails = await _unitOfWork.Repository<CustomerContact>()
                    .GetFilteredAsync(x => contactEmails.Contains(x.Email));
                if (existingContactEmails.Any())
                {
                    return ServiceResult<bool>
                        .Error(new Problem(ErrorCodes.CustomerEmailExists, "One or more contact person emails already exist"));
                }
            }
        }

        return ServiceResult<bool>.Success(true);
    }

    private static Customer CreateCustomerEntity(
        CreateCustomerDto createCustomerDto,
        string customerCode,
        int organisationId,
        string userId)
    {
        var customer = createCustomerDto.Adapt<Customer>();
        customer.CustomerCode = customerCode;
        customer.OrganisationId = organisationId;
        customer.CreatedBy = userId;
        customer.CreatedOn = DateTime.UtcNow;
        return customer;
    }

    private static List<CustomerContact> CreateCustomerContacts(int customerId, CreateCustomerDto createCustomerDto, string userId)
    {
        var contacts = new List<CustomerContact>();

        // Create primary CustomerContact if contact info provided
        if (!string.IsNullOrEmpty(createCustomerDto.FirstName) ||
            !string.IsNullOrEmpty(createCustomerDto.LastName) ||
            !string.IsNullOrEmpty(createCustomerDto.Email) ||
            !string.IsNullOrEmpty(createCustomerDto.PhoneNumber) ||
            !string.IsNullOrEmpty(createCustomerDto.MobileNumber))
        {
            var customerContact = new CustomerContact
            {
                CustomerId = customerId,
                ContactType = ContactType.Primary,
                Salutation = createCustomerDto.Salutation,
                FirstName = createCustomerDto.FirstName,
                LastName = createCustomerDto.LastName,
                Email = createCustomerDto.Email,
                PhoneNumber = createCustomerDto.PhoneNumber,
                MobileNumber = createCustomerDto.MobileNumber,
                IsPrimary = true,
                IsActive = DefaultValues.DefaultIsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            contacts.Add(customerContact);
        }

        // Create additional contact persons
        if (createCustomerDto.ContactPersons?.Any() == true)
        {
            foreach (var contactDto in createCustomerDto.ContactPersons)
            {
                var contactPerson = new CustomerContact
                {
                    CustomerId = customerId,
                    ContactType = contactDto.ContactType,
                    Salutation = contactDto.Salutation,
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Email = contactDto.Email,
                    PhoneNumber = contactDto.PhoneNumber,
                    MobileNumber = contactDto.MobileNumber,
                    IsPrimary = contactDto.IsPrimary,
                    IsActive = contactDto.IsActive,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                contacts.Add(contactPerson);
            }
        }

        return contacts;
    }

    private static List<CustomerAddress> CreateCustomerAddresses(int customerId, CreateCustomerDto createCustomerDto, string userId)
    {
        var addresses = new List<CustomerAddress>();

        // Create billing address if provided
        if (createCustomerDto.BillingAddress != null)
        {
            var billingAddress = new CustomerAddress
            {
                CustomerId = customerId,
                AddressType = AddressType.Billing,
                AddressLine1 = createCustomerDto.BillingAddress.AddressLine1,
                AddressLine2 = createCustomerDto.BillingAddress.AddressLine2,
                City = createCustomerDto.BillingAddress.City,
                StateId = createCustomerDto.BillingAddress.StateId,
                PostalCode = createCustomerDto.BillingAddress.PostalCode,
                CountryId = createCustomerDto.BillingAddress.CountryId,
                IsPrimary = true,
                IsActive = DefaultValues.DefaultIsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            addresses.Add(billingAddress);
        }

        // Create shipping address if provided
        if (createCustomerDto.ShippingAddress != null)
        {
            var shippingAddress = new CustomerAddress
            {
                CustomerId = customerId,
                AddressType = AddressType.Shipping,
                AddressLine1 = createCustomerDto.ShippingAddress.AddressLine1,
                AddressLine2 = createCustomerDto.ShippingAddress.AddressLine2,
                City = createCustomerDto.ShippingAddress.City,
                StateId = createCustomerDto.ShippingAddress.StateId,
                PostalCode = createCustomerDto.ShippingAddress.PostalCode,
                CountryId = createCustomerDto.ShippingAddress.CountryId,
                IsPrimary = createCustomerDto.ShippingAddress.IsPrimary,
                IsActive = DefaultValues.DefaultIsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
            addresses.Add(shippingAddress);
        }

        return addresses;
    }

    private static CustomerFinancial CreateCustomerFinancial(int customerId, CreateCustomerDto createCustomerDto, string userId)
    {
        return new CustomerFinancial
        {
            CustomerId = customerId,
            CreditLimit = createCustomerDto.CreditLimit,
            CreatedBy = userId,
            CreatedOn = DateTime.UtcNow
        };
    }

    private static CustomerTaxInfo CreateCustomerTaxInfo(int customerId, CreateCustomerDto createCustomerDto, string userId)
    {
        if (!string.IsNullOrEmpty(createCustomerDto.Pan))
        {
            return new CustomerTaxInfo
            {
                CustomerId = customerId,
                TaxType = TaxType.PAN,
                TaxNumber = createCustomerDto.Pan,
                IsPrimary = DefaultValues.DefaultIsPrimary,
                IsActive = DefaultValues.DefaultIsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
        }
        return null;
    }

    #endregion
}