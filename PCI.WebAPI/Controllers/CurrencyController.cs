using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class CurrencyController(
    ICurrencyService currencyService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICurrencyService _currencyService = currencyService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetCurrenciesForDropdown()
    {
        var result = await _currencyService.GetCurrenciesForDropdown(OrganisationId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}