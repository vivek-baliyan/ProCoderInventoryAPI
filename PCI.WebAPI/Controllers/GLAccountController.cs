using Microsoft.AspNetCore.Mvc;
using PCI.Application.Services.Interfaces;

namespace PCI.WebAPI.Controllers;

public class GLAccountController(
    IGLAccountService gLAccountService,
    IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IGLAccountService _accountService = gLAccountService;

    [HttpGet("dropdown")]
    public async Task<IActionResult> GetGLAccountsForDropdown()
    {
        var result = await _accountService.GetGLAccountsForDropdown(OrganisationId);

        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status400BadRequest, ErrorResponse(result));
        }

        return StatusCode(StatusCodes.Status200OK, SuccessResponse(result));
    }
}