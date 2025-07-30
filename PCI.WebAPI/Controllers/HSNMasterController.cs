using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class HSNMasterController(
    IHSNMasterService hsnMasterService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IHSNMasterService _hsnMasterService = hsnMasterService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetHSNMastersForDropdown()
    {
        var result = await _hsnMasterService.GetHSNMastersForDropdown();

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}