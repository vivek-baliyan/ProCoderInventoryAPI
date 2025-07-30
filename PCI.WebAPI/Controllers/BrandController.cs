using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class BrandController(
    IBrandService brandService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IBrandService _brandService = brandService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetBrandsForDropdown()
    {
        var result = await _brandService.GetBrandsForDropdown(OrganisationId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}