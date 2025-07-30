using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class TaxMasterController(
    ITaxMasterService taxMasterService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ITaxMasterService _taxMasterService = taxMasterService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetTaxMastersForDropdown()
    {
        var result = await _taxMasterService.GetTaxMastersForDropdown(OrganisationId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}