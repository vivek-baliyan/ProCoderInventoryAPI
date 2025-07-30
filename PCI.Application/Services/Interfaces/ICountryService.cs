using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Interfaces;

public interface ICountryService
{
    Task<ServiceResult<List<DropdownDto>>> GetCountriesForDropdown();
    Task<ServiceResult<List<DropdownDto>>> GetCountriesForDropdownByRegion(string region);
}