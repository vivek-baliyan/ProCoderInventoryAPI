using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class UnitOfMeasureController(
    IUnitOfMeasureService unitOfMeasureService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IUnitOfMeasureService _unitOfMeasureService = unitOfMeasureService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetUnitsForDropdown(int unitType)
    {
        var result = await _unitOfMeasureService.GetUnitsForDropdown(OrganisationId, unitType);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}