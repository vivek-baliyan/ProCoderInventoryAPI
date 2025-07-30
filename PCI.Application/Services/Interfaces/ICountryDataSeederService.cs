using PCI.Shared.Common;

namespace PCI.Application.Services.Interfaces;

public interface ICountryDataSeederService
{
    Task<ServiceResult<bool>> SeedCountriesAndStatesAsync();
    Task<ServiceResult<bool>> UpdateCountryDataAsync();
}