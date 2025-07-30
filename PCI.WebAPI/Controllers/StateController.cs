using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class StateController(
    IStateService stateService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IStateService _stateService = stateService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetStatesForDropdown(int? countryId = null)
    {
        var result = await _stateService.GetStatesForDropdown(countryId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}