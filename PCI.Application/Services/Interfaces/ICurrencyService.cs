using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface ICurrencyService
{
    Task<ServiceResult<List<DropdownDto>>> GetCurrenciesForDropdown(int organisationId);
}