using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class CountryController(
    ICountryService countryService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetCountriesForDropdown()
    {
        var result = await _countryService.GetCountriesForDropdown();

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }

    [HttpGet("dropdown/region/{region}")]
    public async Task<IActionResult> GetCountriesForDropdownByRegion(string region)
    {
        var result = await _countryService.GetCountriesForDropdownByRegion(region);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}