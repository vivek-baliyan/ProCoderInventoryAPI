using PCI.Application.Repositories;
using PCI.Application.Services.Interfaces;
using PCI.Domain.Models;
using PCI.Shared.Common;
using PCI.Shared.Dtos.Common;

namespace PCI.Application.Services.Implementations;

public class CountryService(IUnitOfWork unitOfWork) : ICountryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ServiceResult<List<DropdownDto>>> GetCountriesForDropdown()
    {
        try
        {
            var countries = await _unitOfWork.Repository<Country>().GetAllAsync();

            var result = countries
                .Select(c => new DropdownDto
                {
                    Value = c.Id,
                    Label = c.Name,
                    Code = c.Iso2
                })
                .OrderBy(c => c.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("CountryService.GetCountriesForDropdown", ex.Message));
        }
    }

    public async Task<ServiceResult<List<DropdownDto>>> GetCountriesForDropdownByRegion(string region)
    {
        try
        {
            var countries = await _unitOfWork.Repository<Country>()
                .GetFilteredAsync(c => c.Region == region);

            var result = countries
                .Select(c => new DropdownDto
                {
                    Value = c.Id,
                    Label = c.Name,
                    Code = c.Iso2,
                    AdditionalData = new { c.Region, c.Subregion }
                })
                .OrderBy(c => c.Label)
                .ToList();

            return ServiceResult<List<DropdownDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return ServiceResult<List<DropdownDto>>.Error(new Problem("CountryService.GetCountriesForDropdownByRegion", ex.Message));
        }
    }
}