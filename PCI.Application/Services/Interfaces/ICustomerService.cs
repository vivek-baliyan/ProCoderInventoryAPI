using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;
using PCI.Shared.Dtos.Customer;

namespace PCI.Application.Services.Interfaces;

public interface ICustomerService
{
    Task<ServiceResult<bool>> CreateCustomer(string userId, int organisationId, CreateCustomerDto createCustomerDto);
    Task<ServiceResult<bool>> UpdateCustomer(string userId, int organisationId, UpdateCustomerDto updateCustomerDto);
    Task<ServiceResult<CustomerDto>> GetCustomerById(int id);
    Task<ServiceResult<List<CustomerListItemDto>>> GetAllCustomers(int organisationId, int pageIndex, int pageSize);
    Task<ServiceResult<PaginatedResult<CustomerListItemDto>>> GetFilteredCustomers(int organisationId, CustomerFilterDto filter);
    Task<ServiceResult<bool>> DeleteCustomer(int id, string userId);
    Task<ServiceResult<bool>> ToggleCustomerStatus(int id, string userId);
    Task<ServiceResult<List<CustomerAutocompleteDto>>> GetCustomerAutocomplete(int organisationId, string searchTerm, int limit = 10);
}