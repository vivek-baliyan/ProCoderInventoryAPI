using Mapster;
using Microsoft.EntityFrameworkCore;
using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Application.Specifications;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Common.Constants;
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
            var existingEmail = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(x => x.Email == createCustomerDto.Email && x.OrganisationId == organisationId);

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
                ContactPerson = createCustomerDto.ContactPerson,
                PhoneNumber = createCustomerDto.PhoneNumber,
                MobileNumber = createCustomerDto.MobileNumber,
                Email = createCustomerDto.Email,
                BillingAddress = createCustomerDto.BillingAddress,
                ShippingAddress = createCustomerDto.ShippingAddress,
                City = createCustomerDto.City,
                State = createCustomerDto.State,
                PostalCode = createCustomerDto.PostalCode,
                Country = createCustomerDto.Country,
                WebsiteUrl = createCustomerDto.WebsiteUrl,
                CustomerType = createCustomerDto.CustomerType,
                PaymentTermDays = createCustomerDto.PaymentTermDays,
                CreditLimit = createCustomerDto.CreditLimit,
                TaxNumber = createCustomerDto.TaxNumber,
                GSTNumber = createCustomerDto.GSTNumber,
                PANNumber = createCustomerDto.PANNumber,
                CurrencyId = createCustomerDto.CurrencyId,
                IsActive = createCustomerDto.IsActive,
                Notes = createCustomerDto.Notes,
                OrganisationId = organisationId,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };

            _unitOfWork.Repository<Customer>().Add(customer);
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
            var duplicateEmail = await _unitOfWork.Repository<Customer>()
                .GetFirstOrDefaultAsync(x => x.Email == updateCustomerDto.Email && 
                                            x.Id != updateCustomerDto.Id && 
                                            x.OrganisationId == organisationId);

            if (duplicateEmail != null)
            {
                return ServiceResult<bool>
                    .Error(new Problem(ErrorCodes.CustomerEmailExists, "Customer with this email already exists"));
            }
        }

        try
        {
            existingCustomer.CustomerCode = updateCustomerDto.CustomerCode;
            existingCustomer.CustomerName = updateCustomerDto.CustomerName;
            existingCustomer.CompanyName = updateCustomerDto.CompanyName;
            existingCustomer.ContactPerson = updateCustomerDto.ContactPerson;
            existingCustomer.PhoneNumber = updateCustomerDto.PhoneNumber;
            existingCustomer.MobileNumber = updateCustomerDto.MobileNumber;
            existingCustomer.Email = updateCustomerDto.Email;
            existingCustomer.BillingAddress = updateCustomerDto.BillingAddress;
            existingCustomer.ShippingAddress = updateCustomerDto.ShippingAddress;
            existingCustomer.City = updateCustomerDto.City;
            existingCustomer.State = updateCustomerDto.State;
            existingCustomer.PostalCode = updateCustomerDto.PostalCode;
            existingCustomer.Country = updateCustomerDto.Country;
            existingCustomer.WebsiteUrl = updateCustomerDto.WebsiteUrl;
            existingCustomer.CustomerType = updateCustomerDto.CustomerType;
            existingCustomer.PaymentTermDays = updateCustomerDto.PaymentTermDays;
            existingCustomer.CreditLimit = updateCustomerDto.CreditLimit;
            existingCustomer.TaxNumber = updateCustomerDto.TaxNumber;
            existingCustomer.GSTNumber = updateCustomerDto.GSTNumber;
            existingCustomer.PANNumber = updateCustomerDto.PANNumber;
            existingCustomer.CurrencyId = updateCustomerDto.CurrencyId;
            existingCustomer.IsActive = updateCustomerDto.IsActive;
            existingCustomer.Notes = updateCustomerDto.Notes;
            existingCustomer.ModifiedBy = userId;
            existingCustomer.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Repository<Customer>().Update(existingCustomer);
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
                    includeroperties: "Currency");

            if (customer == null)
            {
                return ServiceResult<CustomerDto>
                    .Error(new Problem(ErrorCodes.CustomerNotFound, "Customer not found"));
            }

            var customerDto = customer.Adapt<CustomerDto>();
            customerDto.CurrencyName = customer.Currency?.Name;
            customerDto.CurrencySymbol = customer.Currency?.Symbol;

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
                    includeroperties: "Currency");

            var customerDtos = customers.Select(c => new CustomerListItemDto
            {
                Id = c.Id,
                CustomerCode = c.CustomerCode,
                CustomerName = c.CustomerName,
                CompanyName = c.CompanyName,
                ContactPerson = c.ContactPerson,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                City = c.City,
                State = c.State,
                Country = c.Country,
                CustomerType = c.CustomerType,
                CreditLimit = c.CreditLimit,
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
                ContactPerson = c.ContactPerson,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                City = c.City,
                State = c.State,
                Country = c.Country,
                CustomerType = c.CustomerType,
                CreditLimit = c.CreditLimit,
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