using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class TaxClassificationController(
    ITaxClassificationService taxClassificationService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ITaxClassificationService _taxClassificationService = taxClassificationService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetTaxClassificationsForDropdown()
    {
        var result = await _taxClassificationService.GetTaxClassificationsForDropdown(OrganisationId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}