using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryDataSeederController(
    ICountryDataSeederService countryDataSeederService) : ControllerBase
{
    private readonly ICountryDataSeederService _countryDataSeederService = countryDataSeederService;

    [HttpPost("seed")]
    public async Task<IActionResult> SeedCountriesAndStates()
    {
        var result = await _countryDataSeederService.SeedCountriesAndStatesAsync();

        if (!result.Succeeded)
        {
            return BadRequest(new { error = result.Problems, message = "Failed to seed country and state data" });
        }

        return Ok(new { message = "Country and state data seeded successfully" });
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateCountryData()
    {
        var result = await _countryDataSeederService.UpdateCountryDataAsync();

        if (!result.Succeeded)
        {
            return BadRequest(new { error = result.Problems, message = "Failed to update country and state data" });
        }

        return Ok(new { message = "Country and state data updated successfully" });
    }
}